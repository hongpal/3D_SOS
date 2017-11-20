using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiEvent : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        genga.check1 = true;
        print(collision.transform.name);
        collision.gameObject.SetActive(false);

    }
}
