using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiEvent : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if(JoyStick.Player == null || collision.transform.name != JoyStick.Player.name )
        {
            genga.check = true;
            JoyStick.Player = null;
            
            for (int i = 0; i < 30; i++)
            {
                genga.block[i].SetActive(false);
            }
            
            for (int i = 0; i <30; i++)
            {
                genga.block[i].SetActive(true);
                genga.block[i].transform.position = genga.block_vector[i];
                genga.block[i].transform.rotation = genga.block_rotate[i];
                genga.block[i].GetComponent<MeshRenderer>().material.color = TouchEvent.c;   
            }
            return;
        }
        genga.check = true;
        print(collision.transform.name);
        collision.gameObject.SetActive(false);
        JoyStick.Player = null;
    }
}
