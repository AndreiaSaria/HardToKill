using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float arrowSpeed = 50f;

    private Animator anim;
    private Transform playerTransform; //Por que isso? Porque sei que se salvar como transform ele vai sofrendo updates.
    //e me salva de ficar fazendo vários vetores. Isso aumenta MUITO a performance
    public GameObject handPlacement;
    public GameObject arrow;
    private GameObject instantiatedArrow;
    private Rigidbody rb;
    private bool dead = false;

    void Start()
    {
        //playerTransform = player.transform;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!dead)
        {

            if (Vector3.Distance(transform.position,playerTransform.position) < 40)
            {//Pensei em fazer um movimento do arqueiro correr, mas fica estranho quando ele corre para longe do player.
                
                //if(Vector3.Distance(transform.position,player.transform.position) < 10 && !Physics.Raycast(this.transform.position,transform.forward, 5f ,scenario))
                //{
                 
                //    Vector3 moveTowards = -player.transform.position;

                //    Quaternion rotation = Quaternion.LookRotation(new Vector3(moveTowards.x, transform.position.y, moveTowards.z) - transform.position);
                //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);

                //    rb.velocity = transform.forward * speed * (1 + Time.deltaTime);

                //    anim.SetBool("Walking", true);
                //    anim.SetBool("Attacking", false);

                //}

                //Agora estou fazendo ele chegar perto do player se estiver muito longe.
                if(Vector3.Distance(transform.position,playerTransform.position) > 20)
                {
                    Quaternion rotation = Quaternion.LookRotation(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z) - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);

                    rb.velocity = transform.forward * speed * (1 + Time.deltaTime);
                }
                else
                {
                    Quaternion rotation = Quaternion.LookRotation(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z) - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);

                    anim.SetBool("Attacking", true);
                    anim.SetBool("Walking", false);

                    rb.velocity = Vector3.zero;
                }
            }
            else
            {
                anim.SetBool("Attacking", false);
                anim.SetBool("Walking", false);

                rb.velocity = Vector3.zero;
            }


        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void DrawArrow()
    {
        instantiatedArrow = Instantiate(arrow, handPlacement.transform, false);
    }

    public void FlyArrow()
    {//colei daqui https://answers.unity.com/questions/178258/detach-from-parent.html
        if(instantiatedArrow.transform.parent != null)
        {
            instantiatedArrow.transform.parent = null; //Tirando do parent
            instantiatedArrow.GetComponent<Rigidbody>().AddForce(transform.forward * arrowSpeed, ForceMode.Impulse);
        }


    }

    public void DamageTaken()
    {
        anim.SetTrigger("Damage");
    }


    public void Death()
    {
        anim.SetBool("Death",true);
        //Invoke("Metodo de menu/ desativar",anim.GetClip("Death").averageDuration)
        dead = true;
    }
}
