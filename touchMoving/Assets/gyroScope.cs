using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gyroScope : MonoBehaviour {
    Vector3 gyroscope_rotation;
 
    void Awake()
    {
        Input.gyro.enabled = true;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    void Update()
    {
        gyroscope_rotation.x += Input.gyro.rotationRateUnbiased.x;
        gyroscope_rotation.y += Input.gyro.rotationRateUnbiased.y;
    }
}


 
