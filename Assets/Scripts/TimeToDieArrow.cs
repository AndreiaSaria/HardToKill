using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToDieArrow : MonoBehaviour
{
    public float time = 3f;
    private void OnEnable()
    {
        Destroy(this.gameObject, time);
    }
    
}
