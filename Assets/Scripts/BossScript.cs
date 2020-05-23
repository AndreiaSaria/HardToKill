using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float rotateSpeed = 9f;
    public GameController controller;

    private Animator anim;
    private Transform playerTransform;
    private Rigidbody rb;
    private bool dead = false;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!dead)
        {
            if (Vector3.Distance(transform.position, playerTransform.position) < 10)
            {
                if (Vector3.Distance(transform.position, playerTransform.position) < 3)
                {
                    Quaternion rotation = Quaternion.LookRotation(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z) - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);

                    anim.SetBool("Attack", true);
                    anim.SetBool("Running", false);
                }
                else
                {
                    anim.SetBool("Running", true);
                    anim.SetBool("Attack", false);

                    Quaternion rotation = Quaternion.LookRotation(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z) - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);

                    rb.velocity = transform.forward * speed * (1 + Time.deltaTime);
                }

            }
            else
            {
                anim.SetBool("Running", false);
            }
        }

    }

    public void DamageTaken() //Somente para receber a chamada
    {

    }


    public void Death() //Se o boss morrer chamar o controller que dá o win e parar de se mover
    {
        anim.SetBool("Dead", true);
        //Invoke("Metodo de menu/ desativar",anim.GetClip("Death").averageDuration)
        dead = true;
        controller.Win();
    }
}
