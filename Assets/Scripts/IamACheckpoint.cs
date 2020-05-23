using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Por algum motivo o new Input system para de receber o movment se damos reset no checkpoint. 
//Logo, estaremos sem checkpoint.
public class IamACheckpoint : MonoBehaviour
{
    public GameController controller;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //controller.CheckPoint();
        }
    }
}
