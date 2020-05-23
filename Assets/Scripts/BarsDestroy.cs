using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Co : MonoBehaviour
{
    public List<GameObject> enemiesThatShouldDie = new List<GameObject>();

    public void KilledOpponent(GameObject opponent)
    {
        if (enemiesThatShouldDie.Contains(opponent))
        {
            enemiesThatShouldDie.Remove(opponent);
            AreOpponentsDead();
        }
       
    }

    public void AreOpponentsDead()
    {
        if (enemiesThatShouldDie.Count <= 0)
        {

            //return true;
        }
        else
        {
            //return false;
        }
    }
}
