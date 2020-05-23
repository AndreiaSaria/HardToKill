using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnemyHit : MonoBehaviour
{
    private Collider hitbox;
    //private GameObject[] enemy;
    private GameObject player;

    private void Start()
    {
        //col = GetComponent<CapsuleCollider> ();
        hitbox = GetComponent<BoxCollider>();
       
    }

    public void AttackHit(int i) //Isto está na animação como um evento
    {
        if (player != null)
        {
            player.GetComponent<HPCount>().Damaged(i);
            //Debug.Log("Hit the player");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Algo entrou na minha vista" + other.name);
        if (other.tag == "Player")
        {
            player = other.gameObject;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            player = null;
        }
    }
}
