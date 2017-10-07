using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public void Start_Ani()
    {
        this.GetComponent<Animation>().Play("Cube-6");
    }
    
}
