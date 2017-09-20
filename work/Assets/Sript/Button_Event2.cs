using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Button_Event2 : MonoBehaviour {
    public GameObject[] Button = new GameObject[3];
    private GameObject[] Block = new GameObject[25 * 3];
    private int[] Ans   = new int[20];

    public void On_Off(int number)
    {
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

        for (int i = 0; i < 25; i++)
        {
            Block[i] = GameObject.CreatePrimitive(PrimitiveType.Plane);
            Block[i].transform.localScale = Scale;
            Block[i].transform.Rotate(Rotate, Space.Self);
            Block[i].transform.position = v;
            v.x += 0.5f;

            if(((i+1)%5) == 0)
            {    
                v.x = -5f;
                v.y -= 0.5f;    
            }
        }

        for(int i = 0; i < 5; i++)
        {
            for (int j = temp1++; j < 20; j += 5)
                Max[i] = System.Math.Max(Max[i], Ans[j]);

            for (int j = 0, k = temp2; j < Max[i]; j++, k -= 5)
                Block[k].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);   
            temp2 += 1;

        }

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

        for (int i = 25; i < 50; i++)
        {
            Block[i] = GameObject.CreatePrimitive(PrimitiveType.Plane);
            Block[i].transform.localScale = Scale;
            Block[i].transform.Rotate(Rotate, Space.Self);
            Block[i].transform.position = v;
            v.x += 0.5f;

            if (((i + 1) % 5) == 0)
            {
                v.x = -1f;
                v.y -= 0.5f;
            }
        }

        for(int i = 30, k = 0; i < 50; i++, k++)
        {
            if(Ans[k] > 0)
                Block[i].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
        }
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

        for (int i = 50; i < 75; i++)
        {
            Block[i] = GameObject.CreatePrimitive(PrimitiveType.Plane);
            Block[i].transform.localScale = Scale;
            Block[i].transform.Rotate(Rotate, Space.Self);
            Block[i].transform.position = v;
            v.x += 0.5f;

            if (((i + 1) % 5) == 0)
            {
                v.x = 3f;
                v.y -= 0.5f;
            }
        }

        for(int i = 0; i < 4; i++)
        {
            for(int j = temp1; j < 5 + temp1; j++)
                Max[i] = System.Math.Max(Max[i], Ans[j]);

            for(int j = 0, k = temp2--; j < Max[i]; j++, k -= 5)
                Block[k].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);

            temp1 += 5;
        }

        // Side 0, 1, 2, 3, 4 
        //      5, 6, 7, 8, 9
    }

    public void Solving_Problems()
    {
        Vector3 Scale  = new Vector3(0.05f, 0.05f, 0.05f);
        Vector3 Rotate = new Vector3(270f, 0, 0);

        for (int i = 0; i < 3; i++)
            Button[i].SetActive(false);

        for (int i = 0; i < 20; i++)
            Ans[i] = Random.Range(0, 6);

        FrontCreate(Scale, Rotate, Ans);
        TopCreate(Scale, Rotate, Ans);
        SideCreate(Scale, Rotate, Ans);

        for (int i = 0; i < 20; i++)
            print(Ans[i]);
    }
}
