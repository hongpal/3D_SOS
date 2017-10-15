using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gyroScope : MonoBehaviour
{
    private int initialOrientationX;
    private int initialOrientationY;
    private int initialOrientationZ;
    public static bool ok = true;
    public static int state = 0;
    // Use this for initialization
    void Start()
    {

        Input.gyro.updateInterval = 0.01f;
        Input.gyro.enabled = ok;
        initialOrientationX = (int)Input.gyro.rotationRateUnbiased.x;
        initialOrientationY = (int)Input.gyro.rotationRateUnbiased.y;
        initialOrientationZ = (int)-Input.gyro.rotationRateUnbiased.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (ok)
        {
            transform.Rotate(initialOrientationX - Input.gyro.rotationRateUnbiased.x,
                            initialOrientationY - Input.gyro.rotationRateUnbiased.y,
                      initialOrientationZ + Input.gyro.rotationRateUnbiased.z);
        }
        switch (state)
        {
            case 0: // 초기화면시
                if (transform.rotation.y > 40 || transform.rotation.y < -40)
                {
                    ok = false;
                }
                else
                {
                    ok = true;
                }
                break;
            case 1: // 도형관찰시
                if (transform.rotation.y > -10 || transform.rotation.y < -70)
                {
                    ok = false;
                }
                else
                {
                    ok = true;
                }
                break;
            case 2: // 블럭쌓기시
                if (transform.rotation.y > 80 || transform.rotation.y < 0)
                {
                    ok = false;
                }
                else
                {
                    ok = true;
                }
                break;
            case 3:
                break;
            case 4:
                break;
            default:
                break;
        }
    }
}