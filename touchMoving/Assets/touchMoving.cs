using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchMoving : MonoBehaviour {

    private Vector2 startPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase) // 손가락 상태
            {
                case TouchPhase.Began: //움직이기 시작할때
                    startPosition = touch.position;
                    break;
                case TouchPhase.Moved: //움직일때
                    gameObject.transform.Rotate((touch.position.y - startPosition.y)*0.005f, - (touch.position.x - startPosition.x) * 0.005f, 0); //x, y축 회전
                    break;
            }
           
        }
	}
}
