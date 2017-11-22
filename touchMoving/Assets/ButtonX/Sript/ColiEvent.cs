using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiEvent : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        genga.check = true;
        print(collision.transform.name);
        collision.gameObject.SetActive(false);
        JoyStick.Player = null;
    }
}
