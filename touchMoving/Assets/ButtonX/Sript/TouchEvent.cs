﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEvent : MonoBehaviour {

    Color c;
    bool check = true;
    int k = 0;
    string name;

    private void Start()
    {
        c = this.GetComponent<MeshRenderer>().material.color;

        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        this.gameObject.GetComponent<Rigidbody>().mass = 40;
        this.gameObject.GetComponent<Rigidbody>().drag = 40;
        name = this.transform.gameObject.name;
        
        if (name.Length < 9)
            k = Convert.ToInt32(name[6]) - 48;
        else
        {
            k = Convert.ToInt32(name.Substring(6, 2));
        }

        genga.check_count++;
    }

    // Update is called once per frame
    void Update()
    {
        
        this.gameObject.GetComponent<Rigidbody>().drag = 1;

        if (genga.check[k] && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                // get the touch position from the screen touch to world point
                Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, this.gameObject.transform.position.z));
                // lerp and set the position of the current object to that of the touch, but smoothly over time.
                genga.block[k].transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime);
                genga.block[k].GetComponent<MeshRenderer>().material.color = Color.black;
            }
        }

    }
}
