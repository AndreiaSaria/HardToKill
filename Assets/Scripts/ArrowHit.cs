using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ArrowHit : MonoBehaviour
{
    private Collider hitbox;
    //private GameObject[] enemy;
    private GameObject player;
    public int damage;
    public AudioClip hit;

    private void Start()
    {
        //col = GetComponent<CapsuleCollider> ();
        hitbox = GetComponentInChildren<BoxCollider>();

    }

    public void AttackHit(int i)
    {
        if (player != null)
        {
            player.GetComponent<AudioSource>().PlayOneShot(hit);
            player.GetComponent<HPCount>().Damaged(i);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            AttackHit(damage);
            Destroy(this.gameObject);
        }

    }
    private void OnTriggerExit(Collider other)
    {

        player = null;

    }
}
