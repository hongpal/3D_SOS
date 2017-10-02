using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInit : MonoBehaviour {
    public static GameObject[] Cube = new GameObject[6];
    public static Vector3[] StartCubeLocation = new Vector3[6];
    

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 6; i++)
        {
            Cube[i] = GameObject.Find("Plane").transform.Find("Cube-" + (i + 1)).gameObject;
            Vector3 v = new Vector3(Cube[i].transform.position.x, Cube[i].transform.position.y, 5);
            StartCubeLocation[i] = v;
        }
    }

    public int Fold(int number, int figurenumber)
    {
        
        switch(figurenumber)
        {
            case 0:
                Animation ani = Cube[number-1].GetComponent<Animation>();
                ani["Cube-" + number].normalizedTime = 0f;
                ani["Cube-" + number].speed = 1.0f;
                ani.Play("Cube-" + number);
                if (number == 4)
                {
                    ani = Cube[number].GetComponent<Animation>();
                    ani.Play("Cube-5-4");
                }
                break;
        }
        
        return --number;
    }

    public int UnFold(int number, int figurenumber)
    {
        print(number);
        switch (figurenumber)
        {
            case 0:
                Animation ani = Cube[number].GetComponent<Animation>();
                ani["Cube-" + (number+1)].normalizedTime = 1f;
                ani["Cube-" + (number+1)].speed = -1.0f;
                ani.Play("Cube-" + (number+1));
                break;
        }

        return ++number;
    }
}
