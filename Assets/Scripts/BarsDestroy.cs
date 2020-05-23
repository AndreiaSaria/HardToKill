using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsDestroy : MonoBehaviour
{
    public GameObject[] enemiesThatShouldDie;

    // Update is called once per frame
    void Update()
    {
        if(enemiesThatShouldDie.Length == 0)
        {
            Destroy(this.gameObject, 1f);
        }
    }
}
