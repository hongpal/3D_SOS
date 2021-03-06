﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Event3 : MonoBehaviour{
    public GameObject[] Button = new GameObject[7];
    public GameObject[] BlockButton = new GameObject[22];
    public GameObject[] temp = new GameObject[100]; // 생성된 큐브가 저장될 배열
    public static int floor; // 층수를 기록하는 변수
    public int count;
    public int block_number;
    public int[] FloorArr = new int[20];
    public Vector3[] v = new Vector3[20];

    public void Start()
    {
        v[0] = new Vector3(-2, 0, 8);
        v[1] = new Vector3(-1, 0, 8);
        v[2] = new Vector3(0, 0, 8);
        v[3] = new Vector3(1, 0, 8);
        v[4] = new Vector3(2, 0, 8);
        v[5] = new Vector3(-2, 0, 7);
        v[6] = new Vector3(-1, 0, 7);
        v[7] = new Vector3(0, 0, 7);
        v[8] = new Vector3(1, 0, 7);
        v[9] = new Vector3(2, 0, 7);
        v[10] = new Vector3(-2, 0, 6);
        v[11] = new Vector3(-1, 0, 6);
        v[12] = new Vector3(0, 0, 6);
        v[13] = new Vector3(1, 0, 6);
        v[14] = new Vector3(2, 0, 6);
        v[15] = new Vector3(-2, 0, 5);
        v[16] = new Vector3(-1, 0, 5);
        v[17] = new Vector3(0, 0, 5);
        v[18] = new Vector3(1, 0, 5);
        v[19] = new Vector3(2, 0, 5);
    }

    //블록 쌓기를 눌렀을 경우 
    public void input(int number)
    {
        floor = number;
        for (int i = 0; i < 7; i++)
            Button[i].SetActive(false);

        for (int i = 0; i < 22; i++)
            BlockButton[i].SetActive(true);
    }

    public void ClickLocation(int number)
    {
        if (FloorArr[number] == count)
        {
            BlockButton[number].GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0);
            FloorArr[number] += 1;
        }
        else if(FloorArr[number] == count+1)
        {
            BlockButton[number].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1);
            FloorArr[number] -= 1;
        }
    }

    public void CreateCube()
    {
        for(int i = 0; i < 20; i++)
        {
            if (FloorArr[i] == 0)
                continue;
            else
            {
                Vector3 tempV = v[i];
                
                for(int k = 0; k < FloorArr[i]; k++)
                {
                    temp[block_number] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    temp[block_number++].transform.position = tempV;
                    tempV.y += 1;
                }
            }
        }

        BlockButton[20].SetActive(true);
    }

    public void Back()
    {
        for (int i = 0; i < block_number; i++)
            Destroy(temp[i]);

        for (int i = 0; i < 20; i++)
            FloorArr[i] = 0;

        floor = count = block_number = 0;

        BlockButton[20].SetActive(false);

        GameObject.Find("Sin-2").GetComponent<Button_Event2>().On_Off(0);

    }

    public void Next()
    {
        floor--;

        if (floor > 0)
        {

            for (int i = 0; i < 20; i++)
            {
                if(FloorArr[i] == count)
                {
                    BlockButton[i].SetActive(false);
                }
                else
                    BlockButton[i].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1);
            }
            count++;
        }

        else
        {
            for (int i = 0; i < 20; i++)
                BlockButton[i].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1);

            for (int i = 0; i < 22; i++)
                BlockButton[i].SetActive(false);

            CreateCube();
        }

    }
    public void On_Off(int number)
    {
        // number가 1일 경우 신 끄기 0일경우 신 켜기
        if (number == 1)
        {
            for (int i = 0; i < 7; i++)
                Button[i].SetActive(false);
            GameObject.Find("Sin-2").GetComponent<Button_Event2>().On_Off(0);
        }
 
        else
        {
            for (int i = 0; i < 7; i++)
                Button[i].SetActive(true);
        }
    }
}
