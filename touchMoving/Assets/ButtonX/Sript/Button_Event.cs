using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Event : MonoBehaviour {

    public GameObject pastObject;
    public GameObject nowObject;
    public static GameObject[] ObjectArr = new GameObject[3];
    public GameObject[] SinObject_1 = new GameObject[6]; //이 캔버스에 있는 버튼 배열
    public bool[] isChange = new bool[3]; // 전개도인 상태 : false 도형 상태 : true
    public Vector3[] StartCubeLocation = new Vector3[6];    
    public static int figureNumber = -1;
    public int Cube_Number = 6;

    public void Start()
    {
        Vector3 v = new Vector3(0, 0, 5);
        
        ObjectArr[0] = GameObject.Find("Figure/Plane");
        ObjectArr[1] = GameObject.Find("Figure/Triangle");
        ObjectArr[2] = GameObject.Find("Figure/Sphere");

        for (int i = 0; i < 3; i++)
        {
            print(i);
            ObjectArr[i].SetActive(false);
            ObjectArr[i].transform.position = v;
            isChange[i] = false;
        }

        pastObject = ObjectArr[0];
    }

    public void ClickEvent(int number)
    {
        figureNumber = number;
        
        if (ObjectArr[number].activeSelf == false)
        {
            if (pastObject.activeSelf == true)
                pastObject.SetActive(false);
            ObjectArr[number].SetActive(true);
        }

        else
        {
            ObjectArr[number].SetActive(false);
            figureNumber = -1;
        }

        pastObject = ObjectArr[number];
    }

    public void CubeChange(bool is_Fold)
    {
        if (is_Fold)
        {
            Cube_Number = GameObject.Find("Figure/Plane").GetComponent<CubeInit>().Fold(Cube_Number, figureNumber);
        }
        else
        {
            Cube_Number = GameObject.Find("Figure/Plane").GetComponent<CubeInit>().UnFold(Cube_Number, figureNumber);
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
                print("a");
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
                print("a");
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
            for (int i = 0; i < 6; i++)
                SinObject_1[i].SetActive(false);

            for (int i = 0; i < 3; i++)
                ObjectArr[i].SetActive(false);
           
            GameObject.Find("Menu").GetComponent<Menu_Event>().On_Off(0);
        }

        else
        {
            for (int i = 0; i < 6; i++)
                SinObject_1[i].SetActive(true);
        }
    }
}
