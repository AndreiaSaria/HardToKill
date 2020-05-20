using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static PlayerInputActions controls;
    public ParticleSystem particle;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotateSpeed = 10f;

    private Vector2 moveInput = Vector3.zero;
    private Vector3 moveTowards;
    private Rigidbody rb;
    private bool mouseUsed = false;
    TerrainCollider terrain;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        terrain = Terrain.activeTerrain.GetComponent<TerrainCollider>();

        controls = new PlayerInputActions();
        controls.PlayerMovement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.PlayerMovement.Move.canceled += ctx => moveInput = Vector2.zero;

    }

    private void Update()
    {
        Debug.DrawRay(moveTowards,Vector3.up , Color.blue, 5f);

        Movement();


        
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable() //Por que não desativar o handler aqui? Porque quando desativamos o script o update também é desativado.
    {
        controls.Disable();
    }

    private bool HandlerForMouseInput()
    {//Colei daqui https://gamedevbeginner.com/how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/#screen_to_world_3d
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;

            if (terrain.Raycast(ray, out hitData, 1000))
            {
                moveTowards = hitData.point;// - new Vector3(transform.position.x, 0, transform.position.z);
                particle.transform.position = moveTowards;

                return true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //moveTowards = Vector3.zero;
            return true;
        }
        return true;
    }

    private void Movement()
    {
        mouseUsed = HandlerForMouseInput();

        if (!mouseUsed || moveInput.magnitude > 0f)
        {
            moveTowards = transform.position; //Por que isso? Porque o vetor que marca a posição no espaço ficaria parado logo que soltamos do mouse e então estaria a puxar o objeto para ele.
            mouseUsed = false;

            rb.velocity = transform.forward * moveSpeed * moveInput.y * (1 + Time.deltaTime);// + transform.right * moveSpeed * moveInput.x; Esta continuação funciona caso não queiramos rodar.
            //Por que eu adiciono 1 ao delta time? Porque sem isso o move speed fica devagar, já que o delta time é normalmente um número decimal

            transform.Rotate(0, moveInput.x * (rotateSpeed * Time.deltaTime * 30), 0); //Aqui a gente só roda em relação a y.
            //Por que eu multipliquei esse rotate speed por 30? Porque rodando pelo input fica bem mais devagar do que fazendo o slerp que fiz na parte do input de mouse.
        }
        else
        {
            if (Vector3.Distance(transform.position, moveTowards) > 1.5f)
            {
                //Forma número 1, fazendo rotação sem speed
                //transform.forward = new Vector3(moveTowards.x, transform.position.y, moveTowards.z) - transform.position ;
                //Qual o motivo disso? Queremos que o objeto vire para onde queremos mas sem sair do eixo de y onde está.
                //Logo, coloco a altura dele em y dentro da posição para onde ele deve virar.

                //Forma número 2, fazendo rotação com speed
                Quaternion rotation = Quaternion.LookRotation(new Vector3(moveTowards.x, transform.position.y, moveTowards.z) - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);

                rb.velocity = transform.forward * moveSpeed * (1 + Time.deltaTime);
            }
            else
            {
                mouseUsed = false;
                rb.velocity = Vector3.zero;
            }
        }
    }
}
