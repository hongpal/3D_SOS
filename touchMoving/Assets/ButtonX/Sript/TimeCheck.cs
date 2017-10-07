﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCheck : MonoBehaviour {

    public static float time;
	// Use this for initialization
	void Start () {
        time = 30f;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(time != 0)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {   
                GameObject.Find("Sin-2").GetComponent<Button_Event2>().Answer();
            }
        }

        int t = Mathf.FloorToInt(time);
        Text text = GetComponent<Text>();
        text.text = "Time : " + t.ToString();
	}
}