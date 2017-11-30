﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour
{
    // 공개
    public static Transform Player;        // 플레이어.
    public Transform Stick;         // 조이스틱.
    //public static GameObject Player;
    public Transform Player1;
    // 비공개
    private Vector3 StickFirstPos;  // 조이스틱의 처음 위치.
    private Vector3 JoyVec;         // 조이스틱의 벡터(방향)
    private float Radius;           // 조이스틱 배경의 반 지름.
    private bool MoveFlag;          // 플레이어 움직임 스위치.

     void Start()
     {
         Radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;
         StickFirstPos = Stick.transform.position;

         // 캔버스 크기에대한 반지름 조절.
         float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
         Radius *= Can;

         MoveFlag = false;
     }

     void Update()
     {
       // if (MoveFlag && Player != null)
            //Player.transform.Translate(Vector3.forward * Time.deltaTime * 1f);
        
     }

     // 드래그
     public void Drag(BaseEventData _Data)
     {
         zoomInAndOut.ok = false;
         MoveFlag = true;
         PointerEventData Data = _Data as PointerEventData;
         Vector3 Pos = Data.position;

         // 조이스틱을 이동시킬 방향을 구함.(오른쪽,왼쪽,위,아래)
         JoyVec = (Pos - StickFirstPos).normalized;

         // 조이스틱의 처음 위치와 현재 내가 터치하고있는 위치의 거리를 구한다.
         float Dis = Vector3.Distance(Pos, StickFirstPos);

         // 거리가 반지름보다 작으면 조이스틱을 현재 터치하고 있는 곳으로 이동.
         if (Dis < Radius)
             Stick.position = StickFirstPos + JoyVec * Dis;
         // 거리가 반지름보다 커지면 조이스틱을 반지름의 크기만큼만 이동.
         else
             Stick.position = StickFirstPos + JoyVec * Radius;
        Vector3 v = new Vector3(0, 0, 0);
        v.x = JoyVec.x * 0.05f;
        v.z = JoyVec.y * 0.05f;
        Player.position += v;
        v = Player.position;
        if (Network.isClient)
        {
            int k = Int32.Parse(GetMiddleString(Player.name, "(", ")"));
            GameObject.Find("Net").GetComponent<NetworkManager>().jenga_move(k, v);
            //genga.block[k].GetComponent<TouchEvent>().move_event(k, v);
        }
        print(Player.position);

        //Player.eulerAngles = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg, 0);
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

    // 드래그 끝.
    public void DragEnd()
     {
         zoomInAndOut.ok = true;
         Stick.position = StickFirstPos; // 스틱을 원래의 위치로.
         JoyVec = Vector3.zero;          // 방향을 0으로.
         MoveFlag = false;
     }
}
