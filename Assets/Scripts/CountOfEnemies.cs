using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountOfEnemies : MonoBehaviour
{
    public int enemiesToKill;
    public GameObject barsToDestroy;

    public void KilledEnemy()
    {
        enemiesToKill--;
        if(enemiesToKill <= 0)
        {
            Destroy(barsToDestroy, 1f);
        }
    }

}
