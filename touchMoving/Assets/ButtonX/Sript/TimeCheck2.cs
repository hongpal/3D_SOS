using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCheck2 : MonoBehaviour {

    public static float time;
    // Use this for initialization
    void Start()
    {
        time = 4f;
    }

    // Update is called once per frame
    void Update()
    {

        if (time != 0)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                time = 4;
                GameObject.Find("Net").GetComponent<NetworkManager>().Game_Start();
            }
        }

        int t = Mathf.FloorToInt(time);
        Text text = GetComponent<Text>();
        text.text = t.ToString();
    }
}
