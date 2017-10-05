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
                switch (number)
                {
                    case 2:
                        ani = Cube[number + 1].GetComponent<Animation>();
                        ani["Cube-4-2"].normalizedTime = 0f;
                        ani["Cube-4-2"].speed = 1.0f;
                        ani.Play("Cube-4-2");
                        ani = Cube[number + 2].GetComponent<Animation>();
                        ani["Cube-5-2"].normalizedTime = 0f;
                        ani["Cube-5-2"].speed = 1.0f;
                        ani.Play("Cube-5-2");
                        break;
                    case 3:
                        ani = Cube[number+2].GetComponent<Animation>();
                        ani["Cube-6-3"].normalizedTime = 0f;
                        ani["Cube-6-3"].speed = 1.0f;
                        ani.Play("Cube-6-3");
                        break;
                    case 4:
                        ani = Cube[number].GetComponent<Animation>();
                        ani["Cube-5-4"].normalizedTime = 0f;
                        ani["Cube-5-4"].speed = 1.0f;
                        ani.Play("Cube-5-4");
                        break;
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
                switch (number)
                {
                    case 1:
                        ani = Cube[number + 2].GetComponent<Animation>();
                        ani["Cube-4-2"].normalizedTime = 1f;
                        ani["Cube-4-2"].speed = -1.0f;
                        ani.Play("Cube-4-2");
                        ani = Cube[number + 3].GetComponent<Animation>();
                        ani["Cube-5-2"].normalizedTime = 1f;
                        ani["Cube-5-2"].speed = -1.0f;
                        ani.Play("Cube-5-2");
                        break;
                    case 2:
                        ani = Cube[number + 3].GetComponent<Animation>();
                        ani["Cube-6-3"].normalizedTime = 1f;
                        ani["Cube-6-3"].speed = -1.0f;
                        ani.Play("Cube-6-3");
                        break;
                    case 3:
                        ani = Cube[number+1].GetComponent<Animation>();
                        ani["Cube-5-4"].normalizedTime = 1f;
                        ani["Cube-5-4"].speed = -1.0f;
                        ani.Play("Cube-5-4");
                        break;
                }
                break;
        }

        return ++number;
    }
}
