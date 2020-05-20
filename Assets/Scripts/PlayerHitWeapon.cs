using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class PlayerHitWeapon : MonoBehaviour
{
    private Collider col;
    private Collider hitbox;
    private GameObject enemy;

    private void Start () {
        col = GetComponent<CapsuleCollider> ();
        hitbox = GetComponentInChildren<BoxCollider>();
    }

    private void Update ()
    {
        
    }

    public void AttackHit(int i)
    {
        if(enemy != null)
        {
            Debug.Log("EnemyHit " + i);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        enemy = other.gameObject ;
        Debug.Log("Pode Acertar " + enemy.name);
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Saiu de mira");
        enemy = null;
    }
}