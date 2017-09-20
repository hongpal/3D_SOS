﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Button_Event2 : MonoBehaviour {
    public GameObject[] Button = new GameObject[9];
    private GameObject[] Block = new GameObject[16 * 4];
    private Vector3[,] Difficulty = new Vector3[3, 16];
    private Vector3[] Middle = new Vector3[9];
    private Vector3[] Hard = new Vector3[16];
    private int[] Ans   = new int[16];
    private int problem = 0;
    private int Block_Number = 0;

    public void Start()
    {
        Difficulty[0, 0] = new Vector3(-0.5f, -1f, 8f);
        Difficulty[0, 1] = new Vector3(0.5f, -1f, 8f);
        Difficulty[0, 2] = new Vector3(-0.5f, -1f, 7f);
        Difficulty[0, 3] = new Vector3(0.5f, -1f, 7f);

        Difficulty[1, 0] = new Vector3(-1f, -1f, 8f);
        Difficulty[1, 1] = new Vector3(0f, -1f, 8f);
        Difficulty[1, 2] = new Vector3(1f, -1f, 8f);
        Difficulty[1, 3] = new Vector3(-1f, -1f, 7f);
        Difficulty[1, 4] = new Vector3(0f, -1f, 7f);
        Difficulty[1, 5] = new Vector3(1f, -1f, 7f);
        Difficulty[1, 6] = new Vector3(-1f, -1f, 6f);
        Difficulty[1, 7] = new Vector3(0f, -1f, 6f);
        Difficulty[1, 8] = new Vector3(1f, -1f, 6f);

        Difficulty[2, 0]  = new Vector3(-1.5f, -1f, 8f);
        Difficulty[2, 1]  = new Vector3(-0.5f, -1f, 8f);
        Difficulty[2, 2]  = new Vector3(0.5f, -1f, 8f);
        Difficulty[2, 3]  = new Vector3(1.5f, -1f, 8f);
        Difficulty[2, 4]  = new Vector3(-1.5f, -1f, 7f);
        Difficulty[2, 5]  = new Vector3(-0.5f, -1f, 7f);
        Difficulty[2, 6]  = new Vector3(0.5f, -1f, 7f);
        Difficulty[2, 7]  = new Vector3(1.5f, -1f, 7f);
        Difficulty[2, 8]  = new Vector3(-1.5f, -1f, 6f);
        Difficulty[2, 9]  = new Vector3(-0.5f, -1f, 6f);
        Difficulty[2, 10] = new Vector3(0.5f, -1f, 6f);
        Difficulty[2, 11] = new Vector3(1.5f, -1f, 6f);
        Difficulty[2, 12] = new Vector3(-1.5f, -1f, 5f);
        Difficulty[2, 13] = new Vector3(-0.5f, -1f, 5f);
        Difficulty[2, 14] = new Vector3(0.5f, -1f, 5f);
        Difficulty[2, 15] = new Vector3(1.5f, -1f, 5f);
    }

    public void On_Off(int number)
    {
        if(Block_Number != 0)
        {
            for(int i = 0; i < Block_Number; i++)
            {
                Destroy(Block[i]);

            }
            Block_Number = 0;

            Button[3].SetActive(false);

            TimeCheck.time = 30f;

            for (int i = 0; i < 3; i++)
                Button[i].SetActive(true);

            return;
        }

        // number가 1일 경우 신 끄기 0일경우 신 켜기
        if (number == 1)
        {
            for (int i = 0; i < 3; i++)
                Button[i].SetActive(false);
            GameObject.Find("Menu").GetComponent<Menu_Event>().On_Off(0);
        }

        else 
        {
            for (int i = 0; i < 3; i++)
                Button[i].SetActive(true);
        }
        
        
    }

    public void MakeBlock()
    {
        for (int i = 0; i < 3; i++)
            Button[i].SetActive(false);
        GameObject.Find("Sin-3").GetComponent<Button_Event3>().On_Off(0);
    }

    public void FrontCreate(Vector3 Scale, Vector3 Rotate, int[] Ans)
    {
        Vector3 v = new Vector3(-5f, 4f, 10f);
        int[] Max = new int[5];
        int temp1 = 0;
        int temp2 = 20;

        for(int i = 0; i < 5; i ++)
            Max[i] = 0;

        for (int i = 0; i < 16; i++)
        {
            Block[i] = GameObject.CreatePrimitive(PrimitiveType.Plane);
            Block[i].transform.localScale = Scale;
            Block[i].transform.Rotate(Rotate, Space.Self);
            Block[i].transform.position = v;
            v.x += 0.8f;

            if(((i+1)%4) == 0)
            {    
                v.x = -5f;
                v.y -= 0.8f;    
            }
        }

       /* for(int i = 0; i < 5; i++)
        {
            for (int j = temp1++; j < 20; j += 5)
                Max[i] = System.Math.Max(Max[i], Ans[j]);

            for (int j = 0, k = temp2; j < Max[i]; j++, k -= 5)
                Block[k].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);   
            temp2 += 1;

        }*/

        // Front 0, 5, 10, 15
        //        1, 6, 11, 16
        //        2, 7, 12, 17
        //        3, 8, 13, 18
        //        4, 9, 14, 19
       
        //        0 5 10 15 20
        //        1 6 11 16 21
        //        2 7 12 17 22
        //        3 8 13 18 23
        //        4 9 14 19 24
    }

    public void TopCreate(Vector3 Scale, Vector3 Rotate, int[] Ans)
    {
        Vector3 v = new Vector3(-1f, 4f, 10f);

        for (int i = 16; i < 32; i++)
        {
            Block[i] = GameObject.CreatePrimitive(PrimitiveType.Plane);
            Block[i].transform.localScale = Scale;
            Block[i].transform.Rotate(Rotate, Space.Self);
            Block[i].transform.position = v;
            v.x += 0.8f;

            if (((i + 1) % 4) == 0)
            {
                v.x = -1f;
                v.y -= 0.8f;
            }
        }

      /*  for(int i = 30, k = 0; i < 50; i++, k++)
        {
            if(Ans[k] > 0)
                Block[i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
        }*/
        // Top 30, 31, 32 .... 49

    }

    public void SideCreate(Vector3 Scale, Vector3 Rotate, int[] Ans)
    {
        Vector3 v = new Vector3(3f, 4f, 10f);
        int[] Max = new int[5];
        int temp1 = 0;
        int temp2 = 73;

        for (int i = 0; i < 5; i++)
            Max[i] = 0;

        for (int i = 32; i < 48; i++)
        {
            Block[i] = GameObject.CreatePrimitive(PrimitiveType.Plane);
            Block[i].transform.localScale = Scale;
            Block[i].transform.Rotate(Rotate, Space.Self);
            Block[i].transform.position = v;
            v.x += 0.8f;

            if (((i + 1) % 4) == 0)
            {
                v.x = 3f;
                v.y -= 0.8f;
            }
        }

      /*  for(int i = 0; i < 4; i++)
        {
            for(int j = temp1; j < 5 + temp1; j++)
                Max[i] = System.Math.Max(Max[i], Ans[j]);

            for(int j = 0, k = temp2--; j < Max[i]; j++, k -= 5)
                Block[k].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);

            temp1 += 5;
        }*/

        // Side 0, 1, 2, 3, 4 
        //      5, 6, 7, 8, 9
    }

    public void CreateBlock(int number)
    {
        for(int i = 0; i < number*number; i++)
        {
            if (Ans[i] <= 0)
                continue;

            Vector3 temp = Difficulty[number - 2, i];
            print(Difficulty[number - 2, i]);
            for (int k = 0; k < Ans[i]; k++)
            {
                
                Block[Block_Number] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Block[Block_Number].transform.position = temp;
                int n = Random.Range(0, 5);
                switch (n)
                {
                    case 0:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.red;
                        break;
                    case 1:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.blue;
                        break;
                    case 2:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.green;
                        break;
                    case 3:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.yellow;
                        break;
                    case 4:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.black;
                        break;
                }
                temp.y += 1;
            }
        }

        problem = 0;

    }

    public void Problem_Check(int number)
    {
        problem = number;

        Button[7].SetActive(false);
        Button[8].SetActive(false);

        for (int i = 4; i < 7; i++)
            Button[i].SetActive(true);
        
    }

    public void Difficulty_Check(int number)
    {
        for (int i = 4; i < 7; i++)
        {
            Button[i].SetActive(false);
        }

        for (int i = 0; i < number*number; i++)
            Ans[i] = Random.Range(0, number+1);

        CreateBlock(number);

        Button[1].SetActive(true);
        Button[3].SetActive(true);
    }


    public void Solving_Problems()
    {
        /*Vector3 Scale  = new Vector3(0.08f, 0.08f, 0.08f);
        Vector3 Rotate = new Vector3(270f, 0, 0);*/

        for (int i = 0; i < 3; i++)
            Button[i].SetActive(false);

        Button[7].SetActive(true);
        Button[8].SetActive(true);  

       /* FrontCreate(Scale, Rotate, Ans);
        TopCreate(Scale, Rotate, Ans);
        SideCreate(Scale, Rotate, Ans);/*

        for (int i = 0; i < 20; i++)
            print(Ans[i]);*/

        
    }
}