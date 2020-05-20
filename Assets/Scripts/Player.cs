using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static PlayerInputActions controls;
    [SerializeField] private float moveSpeed = 3f;
    private Vector2 moveInput;
    private Vector3 moveTowards;
    private Rigidbody rb;
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
        HandlerForMouseInput();

        transform.Translate(moveTowards.normalized * moveSpeed * Time.deltaTime);

    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * moveSpeed * moveInput.y + transform.right * moveSpeed * moveInput.x; 
        
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable() //Por que não desativar o handler aqui? Porque quando desativamos o script o update também é desativado.
    {
        controls.Disable();
    }

    private void HandlerForMouseInput()
    {//Colei daqui https://gamedevbeginner.com/how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/#screen_to_world_3d
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;

            if (terrain.Raycast(ray, out hitData, 1000))
            {
                moveTowards = hitData.point - new Vector3(transform.position.x, 0, transform.position.z);
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            moveTowards = Vector3.zero;
        }

    }

}
