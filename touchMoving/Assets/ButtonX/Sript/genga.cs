using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genga : MonoBehaviour {
    public GameObject[] block = new GameObject[30];
    Vector3 v = new Vector3(-1, -1.7f, 5);
    int block_count = 0;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < 10; i++)
        {
            if(i % 2 == 0)
            {
                for(int k = 0; k < 3; k++)
                {
                    block[block_count] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    block[block_count].transform.position = v;
                    block[block_count].transform.localScale = new Vector3(1, 0.35f, 3);
                    block_count++;
                    v.x += 1.001f;
                }
            }
            else
            {
                v.x = 0;
                v.z = 4;
                for (int k = 0; k < 3; k++)
                {
                    block[block_count] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    block[block_count].transform.position = v;
                    block[block_count].transform.localScale = new Vector3(1, 0.35f, 3);
                    block[block_count].transform.Rotate(0, 90, 0);
                    block_count++;
                    v.z += 1.001f;
                }
                v.x = -1;
                v.z = 5;
            }
            
            v.y += 0.351f;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
