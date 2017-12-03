using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomInAndOut : MonoBehaviour
{
    private string touchStatus; // 손가락 상태
    private float initTouchDistance; // 두 손가락 거리
    public static Vector3 pivot = new Vector3(0, 0, 5); // 화면이동시 중심점  
    private Vector2 startPosition; // 손가락 시작 좌표
    public float horizontalSpeed = 360.0F;
    public float verticalSpeed = 120.0F;
    public static float x, y, distance = 0.0F;
    public static bool ok = false;
    
    public Vector3 Locatino;
   
    // Use this for initialization

    void Start()
    {
        touchStatus = "idle";
        x = transform.eulerAngles.y;
        y = transform.eulerAngles.x;
        distance = (transform.position - pivot).magnitude; //카메라와 중심간의 거리
        Locatino = this.transform.position;
        
    }

    void Update()
    {
        Camera camera = GetComponent<Camera>();

        if (ok)
        {
           if (Input.touchCount > 1) //손가락 터치 2개시
            {
                if (Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position) > initTouchDistance)
                {
                    touchStatus = "zoomIn";
                    camera.fieldOfView -= 0.5f;
                }
                else if (Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position) < initTouchDistance)
                {
                    touchStatus = "zoomOut";
                    camera.fieldOfView += 0.5f;
                }
                initTouchDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }

            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase) // 손가락 상태
                {
                    case TouchPhase.Began: //움직이기 시작할때
                        startPosition = touch.position;
                        break;
                    case TouchPhase.Moved: //움직일때
                                           //gameObject.transform.Rotate((touch.position.y - startPosition.y) * 0.005f, -(touch.position.x - startPosition.x) * 0.005f, 0); //x, y축 회전
                        float dt = Time.deltaTime;//지난 프레임이 완료되는데 걸린 시간(sec 단위)
                        x -= (touch.position.y - startPosition.y) * horizontalSpeed * dt * 0.0007f;
                        y += (touch.position.x - startPosition.x) * verticalSpeed * dt * 0.005f;

                        Quaternion rotation = Quaternion.Euler(x, y, 0); //오일러각도
                        Vector3 position = rotation * (new Vector3(0.0F, 0.0F, -distance)) + pivot;
                        transform.rotation = rotation;
                        transform.position = position;
                        break;
                }
                
            }
        }
        else
        {
           // this.transform.position = Locatino;
         //   this.transform.rotation = Quaternion.identity;
        }
       

    }

}