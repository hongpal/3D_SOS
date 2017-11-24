﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEvent : MonoBehaviour {

    public static Color c;
   
    private void Start()
    { 
        c = this.GetComponent<MeshRenderer>().material.color;

        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        this.gameObject.GetComponent<Rigidbody>().mass = 40;
        this.gameObject.GetComponent<Rigidbody>().drag = 1;
    }

}
