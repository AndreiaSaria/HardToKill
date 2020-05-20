using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static PlayerInputActions controls;
    public ParticleSystem particle;
    [SerializeField] private float moveSpeed = 3f;

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
        //controls.PlayerMovement.MoveToPosition.performed += ctx => moveTowards = ctx.ReadValue<Vector2>().normalized;
        //controls.PlayerMovement.MoveToPosition.canceled += ctx => moveTowards = Vector2.zero;

    }
    private void Update()
    {


        //transform.Translate(moveTowards.normalized * moveSpeed * Time.deltaTime);

    }

    private void FixedUpdate()
    {
        Debug.DrawRay(moveTowards,Vector3.up , Color.blue, 5f);
        mouseUsed = HandlerForMouseInput();
        if (!mouseUsed || moveInput.magnitude > 0f)
        {
            moveTowards = transform.position;
            mouseUsed = false;
            rb.velocity = transform.forward * moveSpeed * moveInput.y + transform.right * moveSpeed * moveInput.x;
        }
        else
        {
            if (Vector3.Distance(transform.position, moveTowards) > 2f)
            {
                transform.rotation = Quaternion.LookRotation(moveTowards, Vector3.up);
                //transform.forward = moveTowards;
                rb.velocity = transform.forward * moveSpeed;
                //Debug.Log(Vector3.Distance(transform.position, moveTowards));
            }
            else
            {
                mouseUsed = false;
                rb.velocity = Vector3.zero;
            }
        }

        
        
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

}
