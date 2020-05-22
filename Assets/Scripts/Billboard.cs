using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script para fazer a câmera seguir o billboard criado em cada enemy
//Colado daqui https://www.youtube.com/watch?v=RcHvsH1Pf_Q&feature=youtu.be

public class Billboard : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
