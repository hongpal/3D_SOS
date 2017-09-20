using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInit : MonoBehaviour {
    public static GameObject[] Cube = new GameObject[6];
    public static Vector3[] StartCubeLocation = new Vector3[6];
    

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 6; i++)
        {
            Cube[i] = GameObject.Find("Plane").transform.Find("Cube-" + (i + 1)).gameObject;
            Vector3 v = new Vector3(Cube[i].transform.position.x, Cube[i].transform.position.y, 5);
            StartCubeLocation[i] = v;
        }
    }
	
}
