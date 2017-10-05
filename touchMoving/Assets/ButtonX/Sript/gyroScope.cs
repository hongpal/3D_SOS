using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gyroScope : MonoBehaviour {
    private int initialOrientationX;
    private int initialOrientationY;
    private int initialOrientationZ;
    // Use this for initialization
    void Start () {
        Input.gyro.enabled = true;
        Input.gyro.updateInterval = 0.01f;

        initialOrientationX = (int)Input.gyro.rotationRateUnbiased.x;
        initialOrientationY = (int)Input.gyro.rotationRateUnbiased.y;
        initialOrientationZ = (int)-Input.gyro.rotationRateUnbiased.z;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(initialOrientationX - Input.gyro.rotationRateUnbiased.x,
                        initialOrientationY - Input.gyro.rotationRateUnbiased.y,
                        initialOrientationZ + Input.gyro.rotationRateUnbiased.z);
    }
}
