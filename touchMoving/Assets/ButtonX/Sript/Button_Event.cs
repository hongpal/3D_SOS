using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Event : MonoBehaviour {

    public GameObject pastObject;
    public GameObject nowObject;
    public static GameObject[] ObjectArr = new GameObject[7];
    public GameObject[] SinObject_1 = new GameObject[3]; //이 캔버스에 있는 버튼 배열
    public Vector3[] StartCubeLocation = new Vector3[6];    
    public static int figureNumber = -1;
    public int Cube_Number = 6;
    public int Triangle_Number = 4;

    public void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(Screen.width, Screen.width * 1280 / 800, true);
    }

    public void Start()
    {
        Vector3 v = new Vector3(0, 0, 0);
        
        ObjectArr[0] = GameObject.Find("Figure/Plane");
        ObjectArr[1] = GameObject.Find("Figure/Triangle");
        ObjectArr[2] = GameObject.Find("Figure/Sphere");

        for (int i = 0; i < 3; i++)
        {
            ObjectArr[i].SetActive(false);
            ObjectArr[i].transform.position = v;
        }

        pastObject = ObjectArr[0];
    }

    public void ClickEvent(int number)
    {
        figureNumber = number;
        gyroScope.ok = false;
        if (ObjectArr[number].activeSelf == false)
        {
            if (pastObject.activeSelf == true)
                pastObject.SetActive(false);
            ObjectArr[number].SetActive(true);

            for (int i = 0; i < 3; i++)
                SinObject_1[i].SetActive(false);

            for (int i = 3; i < 6; i++)
                SinObject_1[i].SetActive(true);
            zoomInAndOut.ok = true;
            

            switch (figureNumber)
            {
                case 0:
                    ObjectArr[figureNumber].transform.position = new Vector3(0, 0,5);
                    SinObject_1[6].transform.position = new Vector3(0, 0, 0);
                    zoomInAndOut.pivot = new Vector3(0, 0, 5);
                    SinObject_1[6].transform.LookAt(new Vector3(0, 0, 5));
                    break;
                case 1:
                    ObjectArr[figureNumber].transform.position = new Vector3(0, 0, 5);
                    SinObject_1[6].transform.position = new Vector3(0, 0, 0);
                    zoomInAndOut.pivot = new Vector3(0, 0, 2.5f);
                    SinObject_1[6].transform.LookAt(new Vector3(0, 0, 5));
                    break;
                case 2:
                    SinObject_1[6].transform.position = new Vector3(0, 0, 0);
                    SinObject_1[6].transform.LookAt(new Vector3(0, 0, 5));
                    break;
                default:
                    print("bobo");
                    break;
            }

        }

        else
        {
            ObjectArr[number].SetActive(false);
        }

        pastObject = ObjectArr[number];
    }

    public void CubeChange(bool is_Fold)
    {
        if (is_Fold)
        {
            Cube_Number = GameObject.Find("Figure").GetComponent<CubeInit>().Fold(Cube_Number, figureNumber);
        }
        else
        {
            Cube_Number = GameObject.Find("Figure").GetComponent<CubeInit>().UnFold(Cube_Number, figureNumber);
        }
    }

    public void TriangleChange(bool is_Fold)
    {
        if (is_Fold)
        {
            Triangle_Number = GameObject.Find("Figure").GetComponent<CubeInit>().Fold(Triangle_Number, figureNumber);
        }
        else
        {
            Triangle_Number = GameObject.Find("Figure").GetComponent<CubeInit>().UnFold(Triangle_Number, figureNumber);
        }
    }

    public void Fold()
    {
        switch (figureNumber)
        {
            case 0:
                CubeChange(true);
                break;
            case 1:
                TriangleChange(true);
                break;
            case 2:
                print("s");
                break;
            default:
                print("bobo");
                break;
        }
    }

    public void UnFold()
    {
        switch (figureNumber)
        {
            case 0:
                CubeChange(false);
                break;
            case 1:
                TriangleChange(false);
                break;
            case 2:
                print("s");
                break;
            default:
                print("bobo");
                break;
        }
    }

    public void On_Off(int number)
    {
        // number가 1일 경우 신 끄기 0일경우 신 켜기
        if(number == 1)
        {   
            switch (figureNumber)
            {
                case 0:
                    for(int i = 1; i < 6; i++)
                        GameObject.Find("Figure").GetComponent<CubeInit>().UnFold(i, 0);
                    Cube_Number = 6;
                    ObjectArr[figureNumber].transform.position = new Vector3(0, 100, -100);
                    break;
                case 1:
                    ObjectArr[figureNumber].transform.position = new Vector3(0, 100, -100);
                    break;
                case 2:
                    print("s");
                    break;
                default:
                    print("bobo");
                    break;
            }


            for (int i = 0; i < 6; i++)
                SinObject_1[i].SetActive(false);

            GameObject.Find("Menu").GetComponent<Menu_Event>().On_Off(0);
            SinObject_1[6].transform.position = new Vector3(0, 0, 0);
            SinObject_1[6].transform.LookAt(new Vector3(0, 0, 5));

            zoomInAndOut.ok = false;
            gyroScope.ok = true;
        }

        else
        {
            for (int i = 0; i < 3; i++)
                SinObject_1[i].SetActive(true);
        }
    }
}
