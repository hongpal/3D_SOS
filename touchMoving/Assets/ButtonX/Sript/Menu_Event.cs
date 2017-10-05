using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 메뉴 버튼 이벤트 정의
*/
public class Menu_Event : MonoBehaviour {
    public GameObject[] Button = new GameObject[2];

    public void On_Off(int number)
    {
       // number에 따른 이벤트 정의
        // 0 -> 메인 메뉴로 돌아옴
        // 1 -> 도형 관찰로 전환
        // 2 -> 블록 쌓기로 전환
        if (number == 1)
        {
            for (int i = 0; i < 2; i++)
                Button[i].SetActive(false);
            GameObject.Find("Sin-1").GetComponent<Button_Event>().On_Off(0);
        }
        else if(number == 2)
        {
            for (int i = 0; i < 2; i++)
                Button[i].SetActive(false);
            GameObject.Find("Sin-2").GetComponent<Button_Event2>().On_Off(0);
        }
        else
        {
            for (int i = 0; i < 2; i++)
                Button[i].SetActive(true);    
        }
    }
}
