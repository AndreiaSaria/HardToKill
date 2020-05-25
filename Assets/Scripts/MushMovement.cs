using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotateSpeed = 5f;

    private Animation anim;
    private Transform playerTransform;
    private Rigidbody rb;
    private bool dead = false;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animation>();
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
                    if (!anim.IsPlaying("Damage"))
                    {
                        //anim.PlayQueued("Attack", QueueMode.PlayNow, PlayMode.StopAll);
                        anim.Play("Attack");
                    }

                }
                else
                {
                    anim.Play("Run");

                    Quaternion rotation = Quaternion.LookRotation(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z) - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);

                    rb.velocity = transform.forward * speed * (1 + Time.deltaTime);
                }

            }
            else
            {

                anim.Play("Idle");

            }
        }

    }

    public void DamageTaken()
    {
        if (!dead)
        {
            anim.PlayQueued("Damage", QueueMode.PlayNow, PlayMode.StopAll);
            //anim.Play("Damage");
        }

    }

  
    public void Death()
    {
        anim.PlayQueued("Death",QueueMode.PlayNow,PlayMode.StopAll);
        //Invoke("Metodo de menu/ desativar",anim.GetClip("Death").averageDuration)
        dead = true;
    }
}
