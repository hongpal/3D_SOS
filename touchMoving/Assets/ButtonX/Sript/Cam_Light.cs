using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Light : MonoBehaviour {

    public GameObject Cam;
	
	// Update is called once per frame
	void Update () {
        this.transform.position = Cam.transform.position;
        //this.transform.rotation = Cam.transform.rotation;
        this.transform.LookAt(new Vector3(0, -12, 5));
	}
}
