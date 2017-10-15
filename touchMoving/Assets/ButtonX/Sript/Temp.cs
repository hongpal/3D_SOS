using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Temp : MonoBehaviour {
    public GameObject[] temp = new GameObject[2];
    // Use this for initialization

    // Update is called once per frame
    void Update () {
	    temp[0].transform.localScale = new Vector3(0, 0, 0);
        temp[1].transform.localScale = new Vector3(0, 0, 0);
    }
}
