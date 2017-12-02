using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genga : MonoBehaviour {
    public static GameObject[] block = new GameObject[30];
    public static bool check = true;
    public static Vector3[] block_vector = new Vector3[30];
    public static Quaternion[] block_rotate = new Quaternion[30];
    int k = 0;

    private void Start()
    {
        if (this.gameObject.name != "GenGa")
        {
            ColiEvent.jenga = this.gameObject;
            return;
        }
        for (int i = 0; i < 30; i++)
        {
            block[i] = GameObject.Find("GenGa/Cube (" + i +")");
            block_vector[i] = block[i].transform.position;
            block_rotate[i] = block[i].transform.rotation;
        }
       
    }

    // Update is called once per frame
    void Update () {
        if (TouchScreenKeyboard.isSupported)
        {
            if (Input.touchCount > 0 && check)
            {
                Vector2 pos = Input.GetTouch(0).position;    // 터치한 위치

                Vector3 theTouch = new Vector3(pos.x, pos.y, 0.0f);    // 변환 안하고 바로 Vector3로 받아도 되겠지.

                Ray ray = Camera.main.ScreenPointToRay(theTouch);    // 터치한 좌표 레이로 바꾸엉

                RaycastHit hit;    // 정보 저장할 구조체 만들고

                if (Physics.Raycast(ray, out hit))    // 레이저를 끝까지 쏴블자. 충돌 한넘이 있으면 return true다.
                {
                    Touch touch = Input.GetTouch(0);

                    if (Input.GetTouch(0).phase == TouchPhase.Began)    // 딱 처음 터치 할때 발생한다
                    {
                        print(hit.transform.gameObject.name);

                        if (hit.transform.gameObject.name == "Cubetest" || hit.transform.gameObject.name == "Cube")
                            return;

                        if (Button_Event2.net_check == 1)
                            if (!NetworkManager.my_turn)
                                return;

                        for (int i = 0; i < 30; i++)
                        {
                            if (hit.transform.gameObject.name.Equals(block[i].name))
                            {
                                JoyStick.Player = block[i].transform;
                                genga.block[i].GetComponent<MeshRenderer>().material.color = Color.black;
                                check = false;
                                if (Button_Event2.net_check == 1)
                                {
                                    GameObject.Find("Net").GetComponent<NetworkManager>().select(i);
                                }
                                break;
                            }
                        }

                    }

                    else if (Input.GetTouch(0).phase == TouchPhase.Moved)    // 터치하고 움직이믄 발생한다.
                    {

                    }

                    else if (Input.GetTouch(0).phase == TouchPhase.Ended)    // 터치 따악 떼면 발생한다.
                    {
                        //this.GetComponent<MeshRenderer>().material.color = c;
                    }

                }

            }
            return;
        }

    }
}
