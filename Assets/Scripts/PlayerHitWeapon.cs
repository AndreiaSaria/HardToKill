using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerHitWeapon : MonoBehaviour
{

    private Collider hitbox;
    private List<GameObject> enemy = new List<GameObject>(); //Por que uma lista? Porque mais de um inimigo pode estar na range de ataque;

    private void Start () {
        //col = GetComponent<CapsuleCollider> ();
        hitbox = GetComponentInChildren<BoxCollider>();
        
    }

    public void AttackHit(int i)
    {
        if(enemy != null)
        {
            foreach (GameObject g in enemy)
            {
                if (g != null)//Por que isso? Porque o inimigo pode ter sido destroyed antes do attack hit.
                {
                    GetComponent<SetSounds>().HitOther();
                    g.GetComponent<HPCount>().Damaged(i);
                }
                else
                {
                    enemy.Remove(g);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {//colei daqui https://answers.unity.com/questions/665241/affecting-multiple-objects-on-a-trigger.html

        if (other.tag == "Enemy" && !enemy.Contains(other.gameObject))
        {
            enemy.Add(other.gameObject);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (enemy.Contains(other.gameObject))
        {
            enemy.Remove(other.gameObject);
        }

        
    }
}