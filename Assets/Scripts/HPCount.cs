using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPCount : MonoBehaviour
{
    public int HP;
    
    public void Damaged(int i) //Desse jeito que fiz não dá para ser estático. That just happened.
    {//Sei que passar mensagens não é a melhor forma de se fazer isso. That just happened²

        HP = HP - i;
        //Debug.Log("Hp dropped " + i + "values");

        if (HP <= 0)
        {
            SendMessage("Death");

            if(gameObject.tag != "Player")
            {
                Destroy(gameObject, 4f);
            }
        }
        else
        {
            SendMessage("DamageTaken");
        }
    }

}
