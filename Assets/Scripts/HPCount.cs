using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPCount : MonoBehaviour
{
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    public int HP;
    
    public void Damaged(int i)
    {
        HP = HP - i;
        //Debug.Log("Hp dropped " + i + "values");

        if (HP <= 0)
        {
            SendMessage("Death");
        }
        else
        {
            SendMessage("DamageTaken");
        }
    }

}
