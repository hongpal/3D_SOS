﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEvent : MonoBehaviour {

    public static Color c;
   
    private void Start()
    {
        int k;
        c = this.GetComponent<MeshRenderer>().material.color;

        k = Int32.Parse(GetMiddleString(this.gameObject.name, "(", ")"));
        genga.block[k] = this.gameObject;
        genga.block_vector[k] = genga.block[k].transform.position;
        genga.block_rotate[k] = genga.block[k].transform.rotation;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        this.gameObject.GetComponent<Rigidbody>().mass = 40;
        this.gameObject.GetComponent<Rigidbody>().drag = 1;
    }

    public static string GetMiddleString(string str, string begin, string end)
    {
        if (string.IsNullOrEmpty(str))
        {
            return null;
        }

        string result = null;
        if (str.IndexOf(begin) > -1)
        {
            str = str.Substring(str.IndexOf(begin) + begin.Length);
            if (str.IndexOf(end) > -1) result = str.Substring(0, str.IndexOf(end));
            else result = str;
        }
        return result;
    }

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        Vector3 syncPosition = Vector3.zero;
        
        if (stream.isWriting)
        {
            print("write");
            syncPosition = GetComponent<Rigidbody>().position;
            stream.Serialize(ref syncPosition);
        }
        else
        {
            print("read");
            stream.Serialize(ref syncPosition);
            GetComponent<Rigidbody>().position = syncPosition;
        }
    }
}
