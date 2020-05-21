using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ATENÇÃO! SE QUISER USAR O RAYCAST O OBJETO GROUND TEM DE TER UM COLLIDER DIFERENTE DE MESH. TAMBÉM NÃO DEVE SER UM COLLIDER 2D.

public class PlayerMovement : MonoBehaviour
{
    public static PlayerInputActions controls;
    public ParticleSystem particle;
    public bool tank = false;
    public bool tankWithAim = false;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotateSpeed = 10f;

    private Vector2 moveInput;
    private Vector2 aimInput;
    private Vector3 moveTowards;
    private Rigidbody rb;
    //TerrainCollider terrain;
    private LayerMask ground;
    private Animator anim;
    private float cont;
    //private static int attack = Animator.StringToHash("Base.Attack");

    private void Awake()
    {
        moveTowards = transform.position;
        //Para caso o player apareça em qualquer lugar diferente do inicial

        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();

        //terrain = Terrain.activeTerrain.GetComponent<TerrainCollider>();
        ground = LayerMask.GetMask("Ground");

        controls = new PlayerInputActions();
        controls.PlayerMovement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.PlayerMovement.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.PlayerMovement.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        

        controls.PlayerMovement.Attack.performed += ctx => anim.SetBool("Attack", true);
        controls.PlayerMovement.Attack.canceled += ctx => anim.SetBool("Attack", false);

    }

    private void Update()
    {
        //Debug.DrawRay(moveTowards,Vector3.up , Color.blue, 5f);

        anim.SetBool("Walking", Movement());
        


        //Demonstrar que o new input system não recebe mais de 2teclas ao mesmo tempo

    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable() //Por que não desativar o handler aqui? Porque quando desativamos o script o update também é desativado.
    {
        controls.Disable();
    }

    public void Death()
    {
        Debug.Log("Player dead");
    }

    public void DamageTaken()
    {
        //Debug.Log("Player Took Damage");
    }

    private bool HandlerForMouseInput() //colocar touch aqui
    {//Colei daqui https://gamedevbeginner.com/how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/#screen_to_world_3d

        if (Input.GetMouseButton(0))
        {
            cont += Time.deltaTime;
            if(cont > 0.7f)
            {
                anim.SetBool("Attack", true);
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;

            if (Physics.Raycast(ray, out hitData, 1000, ground))
            {
                moveTowards = hitData.point;// - new Vector3(transform.position.x, 0, transform.position.z);

                //particle.transform.position = moveTowards;
                //particle.Play();
            }


        }

        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("Attack", false);
            cont = 0;
            particle.transform.position = moveTowards;
            particle.Play();
            //moveTowards = Vector3.zero;
        }
        return true;
    }

    private bool Movement()
    {

        HandlerForMouseInput();

        if (moveInput.magnitude > 0f) //Caso estejamos a mover com o joystick
        {
            moveTowards = transform.position; //Por que isso? Porque o vetor que marca a posição no espaço ficaria parado logo que soltamos do mouse e então estaria a puxar o objeto para ele.

            if (tank) //Caso queira um movimento que roda e move usando apenas o stick direito.
            {
                if (moveInput.y > 0.1f)//Por que? Porque recebemos o input como analógico e isso faz com que o vetor movimento seja normalizado. Mas eu quero que ele se mova como se fosse um botão.
                {
                    rb.velocity = transform.forward * moveSpeed * (1 + Time.deltaTime);
                    //Por que eu adiciono 1 ao delta time? Porque sem isso o move speed fica devagar, já que o delta time é normalmente um número decimal
                }
                else if (moveInput.y < -0.1f)
                {
                    rb.velocity = -transform.forward * moveSpeed * (1 + Time.deltaTime);
                }
                transform.Rotate(0, moveInput.x * (rotateSpeed * Time.deltaTime * 30), 0); //Aqui a gente só roda em relação a y.
                //Por que eu multipliquei esse rotate speed por 30? Porque rodando pelo input fica bem mais devagar do que fazendo o slerp que fiz na parte do input de mouse.
            }
            else if(tankWithAim) //Nesse movimento ir para a frente é igual a ir no sentido da rotação.
            {
                rb.velocity = transform.forward * moveSpeed * moveInput.y * (1+Time.deltaTime) + transform.right * moveSpeed * moveInput.x * (1+Time.deltaTime);

                Quaternion rotation = Quaternion.LookRotation(new Vector3(aimInput.x, 0, aimInput.y));
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
            }
            else //Nesse movimento ir para a frente é ir no sentido norte da câmera
            {
                rb.velocity = Vector3.forward * moveSpeed * moveInput.y + Vector3.right * moveSpeed * moveInput.x;

                Quaternion rotation = Quaternion.LookRotation(new Vector3(aimInput.x, 0, aimInput.y));
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
            }

            return true;
        }
        else //Caso estejamos a mover com o mouse
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
                return true;
            }
            else
            {
                rb.velocity = Vector3.zero;
                return false;
            }
            
        }

    }
}
