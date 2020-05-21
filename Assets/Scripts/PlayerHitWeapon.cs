using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerHitWeapon : MonoBehaviour
{
    //private Collider col;
    private Collider hitbox;
    //private GameObject[] enemy;
    private List<GameObject> enemy = new List<GameObject>();

    private void Start () {
        //col = GetComponent<CapsuleCollider> ();
        hitbox = GetComponentInChildren<BoxCollider>();
        
    }

    private void Update ()
    {
        
    }

    public void AttackHit(int i)
    {
        if(enemy != null)
        {
            Debug.Log(enemy.Count +" Is the amount of enemies being hit")
            foreach (GameObject g in enemy)
            {
                g.GetComponent<HPCount>().Damaged(i);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            enemy.Add(other.gameObject);
        }

    }
    private void OnTriggerExit(Collider other)
    {

        enemy.Remove(other.gameObject);
        
    }
}