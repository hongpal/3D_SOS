using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Button_Event2 : MonoBehaviour {

    public GameObject[] Button = new GameObject[17];
    public GameObject[] Easy_Ans = new GameObject[4];
    public GameObject[] Middle_Ans = new GameObject[9];
    public GameObject[] Hard_Ans = new GameObject[16];
    public GameObject Cam;
    private GameObject[] Block = new GameObject[16 * 4];
    private Vector3[,] Difficulty = new Vector3[3, 16];
    private TouchScreenKeyboard keyboard = null;
    public string stringToEdit = "Hi";
    public int[] Ans   = new int[16];
    private int[] Select = new int[16];
    private string text ="Input Number";
    private int temp = 0;
    private int count = 0;
    private int problem = 0;
    private int Block_Number = 0;
    private int Dif = 0;
    private int sum;
    private int net_check = 0;
    private bool is_Ans = false;

    public void Start()
    {
        Difficulty[0, 0] = new Vector3(-0.5f, -2f, 8f); // 0, -1.5, 7.5
        Difficulty[0, 1] = new Vector3(0.5f, -2f, 8f);
        Difficulty[0, 2] = new Vector3(-0.5f, -2f, 7f);
        Difficulty[0, 3] = new Vector3(0.5f, -2f, 7f);

        Difficulty[1, 0] = new Vector3(-1f, -2f, 8f);
        Difficulty[1, 1] = new Vector3(0f, -2f, 8f);
        Difficulty[1, 2] = new Vector3(1f, -2f, 8f);
        Difficulty[1, 3] = new Vector3(-1f, -2f, 7f);
        Difficulty[1, 4] = new Vector3(0f, -2f, 7f);
        Difficulty[1, 5] = new Vector3(1f, -2f, 7f);
        Difficulty[1, 6] = new Vector3(-1f, -2f, 6f);
        Difficulty[1, 7] = new Vector3(0f, -2f, 6f);
        Difficulty[1, 8] = new Vector3(1f, -2f, 6f);

        Difficulty[2, 0]  = new Vector3(-1.5f, -2f, 8f);
        Difficulty[2, 1]  = new Vector3(-0.5f, -2f, 8f);
        Difficulty[2, 2]  = new Vector3(0.5f, -2f, 8f);
        Difficulty[2, 3]  = new Vector3(1.5f, -2f, 8f);
        Difficulty[2, 4]  = new Vector3(-1.5f, -2f, 7f);
        Difficulty[2, 5]  = new Vector3(-0.5f, -2f, 7f);
        Difficulty[2, 6]  = new Vector3(0.5f, -2f, 7f);
        Difficulty[2, 7]  = new Vector3(1.5f, -2f, 7f);
        Difficulty[2, 8]  = new Vector3(-1.5f, -2f, 6f);
        Difficulty[2, 9]  = new Vector3(-0.5f, -2f, 6f);
        Difficulty[2, 10] = new Vector3(0.5f, -2f, 6f);
        Difficulty[2, 11] = new Vector3(1.5f, -2f, 6f);
        Difficulty[2, 12] = new Vector3(-1.5f, -2f, 5f);
        Difficulty[2, 13] = new Vector3(-0.5f, -2f, 5f);
        Difficulty[2, 14] = new Vector3(0.5f, -2f, 5f);
        Difficulty[2, 15] = new Vector3(1.5f, -2f, 5f);
    }

    public void On_Off(int number)
    {
        Button[9].SetActive(false);
        Button[12].SetActive(false);

        if (Block_Number != 0)
        {
            
            for(int i = 0; i < Block_Number; i++)
            {
                Destroy(Block[i]);

            }
            Block_Number = 0;

            Button[3].SetActive(false);

            TimeCheck.time = 30f;

            for (int i = 0; i < 3; i++)
                Button[i].SetActive(true);
           
            for (int i = 0; i < Dif * Dif; i++)
                Ans[i] = Select[i] = 0;
            Cam.transform.position = new Vector3(0, 0, 0);
            Cam.transform.LookAt(new Vector3(5, 0, 6));
            zoomInAndOut.ok = false;
            gyroScope.ok = true;
            sum = problem = Block_Number = Dif = 0;
            return;
        }

        // number가 1일 경우 신 끄기 0일경우 신 켜기
        if (number == 1)
        {
            for (int i = 0; i < 3; i++)
                Button[i].SetActive(false);
            GameObject.Find("Menu").GetComponent<Menu_Event>().On_Off(0);
            zoomInAndOut.ok = false;
            gyroScope.ok = true;
            Cam.transform.position = new Vector3(0, 0, 0);
            Cam.transform.LookAt(new Vector3(0, 0, 5));
        }

        else if(number == 0) 
        {
            for (int i = 0; i < 3; i++)
                Button[i].SetActive(true);

            Button[13].SetActive(false);
            Button[14].SetActive(false);
            net_check = 0;
        }

        else if(number == 2)
        {
            Button[13].SetActive(true);
            Button[14].SetActive(true);
        }
        
        else if(number == 3)
        {
            
            Button[13].SetActive(false);
            Button[14].SetActive(false);

            Button[15].SetActive(true);
            Button[16].SetActive(true);
            net_check = 1;
        }

        else if(number == 4)
        {
            Button[15].SetActive(false);
            Button[16].SetActive(false);
            net_check = 1;
            NetworkManager.is_Ans = true;
        }
    }

    public void MakeBlock()
    {
        for (int i = 0; i < 3; i++)
            Button[i].SetActive(false);
        GameObject.Find("Sin-3").GetComponent<Button_Event3>().On_Off(0);
        Cam.transform.LookAt(new Vector3(0, 0, 5));
        gyroScope.ok = false;
    }

    public void CreateBlock()
    {
        int number = Dif;

        Cam.transform.position = new Vector3(0, 0, 0);
        Cam.transform.LookAt(new Vector3(0, 0, 5));
        zoomInAndOut.pivot = new Vector3(0, 0, 5);
        Button[3].SetActive(true);
        Button[9].SetActive(true);
        zoomInAndOut.ok = true;
        gyroScope.ok = false;

        switch (number)
        {
            case 2:
                zoomInAndOut.pivot = new Vector3(0, -1.5f, 7.5f); //0, -1.5, 7.5
                break;
            case 3:
                break;
            case 4:
                break;

        }


        for (int i = 0; i < number * number; i++)
        {
            if (Ans[i] <= 0)
                continue;

            Vector3 temp = Difficulty[number - 2, i];

            for (int k = 0; k < Ans[i]; k++)
            {

                Block[Block_Number] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Block[Block_Number].transform.position = temp;

                int n = Random.Range(0, 5);
                switch (n)
                {
                    case 0:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.red;
                        break;
                    case 1:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.blue;
                        break;
                    case 2:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.green;
                        break;
                    case 3:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.yellow;
                        break;
                    case 4:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.black;
                        break;
                }
                temp.y += 1;
            }
        }
    }
    
    public void CreateBlock(int number)
    {
        switch (number)
        {
            case 2:
                zoomInAndOut.pivot = new Vector3(0, -1.5f, 7.5f); //0, -1.5, 7.5
                break;
            case 3:
                break;
            case 4:
                break;

        }


        for(int i = 0; i < number*number; i++)
        {
            if (Ans[i] <= 0)
                continue;

            Vector3 temp = Difficulty[number - 2, i];
           
            for (int k = 0; k < Ans[i]; k++)
            {
                
                Block[Block_Number] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Block[Block_Number].transform.position = temp;
                
                int n = Random.Range(0, 5);
                switch (n)
                {
                    case 0:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.red;
                        break;
                    case 1:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.blue;
                        break;
                    case 2:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.green;
                        break;
                    case 3:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.yellow;
                        break;
                    case 4:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.black;
                        break;
                }
                temp.y += 1;
            }
        }
    }

    public void CreateBlock(int number, int[] test)
    {

        Ans = test;
        Cam.transform.position = new Vector3(0, 0, 0);
        Cam.transform.LookAt(new Vector3(0, 0, 5));
        zoomInAndOut.pivot = new Vector3(0, 0, 5);
        Button[3].SetActive(true);
        Button[9].SetActive(true);
        zoomInAndOut.ok = true;
        gyroScope.ok = false;

        switch (number)
        {
            case 2:
                zoomInAndOut.pivot = new Vector3(0, -1.5f, 7.5f); //0, -1.5, 7.5
                break;
            case 3:
                break;
            case 4:
                break;

        }

        for (int i = 0; i < 16; i++)
            print(Ans[i]);

        print("number :" + number);

        for (int i = 0; i < number * number; i++)
        {
            if (Ans[i] <= 0)
                continue;

            Vector3 temp = Difficulty[number - 2, i];

            for (int k = 0; k < Ans[i]; k++)
            {

                Block[Block_Number] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Block[Block_Number].transform.position = temp;

                int n = Random.Range(0, 5);
                switch (n)
                {
                    case 0:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.red;
                        break;
                    case 1:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.blue;
                        break;
                    case 2:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.green;
                        break;
                    case 3:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.yellow;
                        break;
                    case 4:
                        Block[Block_Number++].GetComponent<MeshRenderer>().material.color = Color.black;
                        break;
                }
                temp.y += 1;
            }
        }
    }

    public void Problem_Check(int number)
    {
        problem = number;

        Button[7].SetActive(false);
        Button[8].SetActive(false);

        for (int i = 4; i < 7; i++)
            Button[i].SetActive(true);
        
    }

    public void Difficulty_Check(int number)
    {
        int sum = 0;
        Dif = temp = number;

        for (int i = 4; i < 7; i++)
        {
            Button[i].SetActive(false);
        }
        
        while(sum == 0)
        {
            for (int i = 0; i < number * number; i++)
            {
                Ans[i] = Random.Range(0, number + 1);
                sum += Ans[i];
            }
        }

        if (net_check == 1)
        {
            NetworkManager.Ans = Ans;
            NetworkManager.Dif = Dif;
            GameObject.Find("Net").GetComponent<NetworkManager>().StartServer();
            return;
        }

        CreateBlock(number);

        Button[1].SetActive(true);
        Button[3].SetActive(true);
        Button[9].SetActive(true);
        zoomInAndOut.ok = true;
        gyroScope.ok = false;
        Cam.transform.position = new Vector3(0, 0, 0);
        Cam.transform.LookAt(new Vector3(0, 0, 5));
        zoomInAndOut.pivot = new Vector3(0, 0, 5);
    }

    public void Change_Appear(int number, bool ok)
    {
        if(ok)
        {
            switch (Dif)
            {
                case 2:
                    Easy_Ans[number].SetActive(true);
                    break;
                case 3:
                    Middle_Ans[number].SetActive(true);
                    break;
                case 4:
                    Hard_Ans[number].SetActive(true);
                    break;
            }
        }

        else
        {
            switch (Dif)
            {
                case 2:
                    Easy_Ans[number].SetActive(false);
                    break;
                case 3:
                    Middle_Ans[number].SetActive(false);
                    break;
                case 4:
                    Hard_Ans[number].SetActive(false);
                    break;
            }
        }
    }

    public void Answer()
    {
        TimeCheck.time = 30f;

        for (int i = 0; i < Block_Number; i++)
            Block[i].SetActive(false);

        Button[1].SetActive(false);
        Button[3].SetActive(false);
        Button[9].SetActive(false);

        zoomInAndOut.ok = false;
        gyroScope.ok = false;

        if (problem == 1) // 블록 갯수 맞추기
        {
            sum = 0;
            for (int i = 0; i < Dif * Dif; i++)
                sum += Ans[i];
            is_Ans = true;
            
        }

        else if (problem == 2) // 블록 똑같이 쌓기
        {
            for (int i = 0; i < Dif * Dif; i++)
            {
                Change_Appear(i, true);
            }

            Button[10].SetActive(true);
        }
    }

    void OnGUI()
    {

        if (is_Ans)
        {
            stringToEdit = "";
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumbersAndPunctuation, false, false, false, false);

            is_Ans = false;

        }

        if (keyboard != null && keyboard.done)
         {
            stringToEdit = keyboard.text;

            keyboard = null;

            Check_Ans();
        }        
    }

    public void Correct()
    {
        for (int i = 0; i < Block_Number; i++)
            Destroy(Block[i]);

        for(int i = 0; i < Dif *Dif; i++)
            Ans[i] = Select[i] = 0;

        sum = problem = Block_Number = Dif = 0;

        for (int i = 0; i < 3; i++)
            Button[i].SetActive(true);


        Button[11].SetActive(false);
       
        zoomInAndOut.ok = false;
        gyroScope.ok = true;
        Cam.transform.position = new Vector3(0, 0, 0);
        Cam.transform.LookAt(new Vector3(5, 0, 6));
    }

    public void Wrong()
    {
        TimeCheck.time = 30f;
        stringToEdit = "";
        for (int i = 0; i < Block_Number; i++)
            Block[i].SetActive(true);

        Button[1].SetActive(true);
        Button[3].SetActive(true);
        Button[9].SetActive(true);
        
        Button[12].SetActive(false);

        for (int i = 0; i < Dif*Dif; i++)
            Select[i] = 0;

        zoomInAndOut.ok = true;
        gyroScope.ok = false;
        Cam.transform.position = new Vector3(0, 0, 0);
        Cam.transform.LookAt(new Vector3(0, 0, 5));
    }

    public void Check_Ans()
    {
        int num = 0;
        
        if (sum != 0)
        {
            num = int.Parse(stringToEdit);
            stringToEdit = "";
            if (num == sum) // 정답
                Button[11].SetActive(true);
            else  // 틀림
            {
                Button[1].SetActive(true);
                Button[12].SetActive(true);
            }
        }

        else
        {
            for (int i = 0; i < Dif * Dif; i++)
            {
                if (Ans[i] == Select[i])
                    num++;
            }

            if (num == Dif * Dif)
                Button[11].SetActive(true);
            else
            {
                Button[1].SetActive(true);
                Button[12].SetActive(true);
            }
        }
    }

    public void Change_Color_BLock(int number)
    {
        switch (Dif)
        {
            case 2:
                Easy_Ans[number].GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0);
                break;
            case 3:
                Middle_Ans[number].GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0);
                break;
            case 4:
                Hard_Ans[number].GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0);
                break;
        }
    }

    public void Change_Color_White(int number)
    {
        switch (Dif)
        {
            case 2:
                Easy_Ans[number].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1);
                break;
            case 3:
                Middle_Ans[number].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1);
                break;
            case 4:
                Hard_Ans[number].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1);
                break;
        }
    }

    public void ClickLocation(int number)
    {
        if (Select[number] == count)
        {
            Change_Color_BLock(number);
            Select[number] += 1;
        }
        else if (Select[number] == count + 1)
        {
            Change_Color_White(number);
            Select[number] -= 1;
        }
    }

    public void Next()
    {
        temp--;

        if (temp > 0)
        {
            for (int i = 0; i < Dif*Dif; i++)
            {
                if (Select[i] == count)
                {
                    Change_Appear(i, false);
                }
                else
                    Change_Color_White(i);
            }
            count++;
        }

        else
        {
            for (int i = 0; i < Dif * Dif; i++)
            {
                Change_Color_White(i);
            }
            for (int i = 0; i < Dif*Dif; i++)
                Change_Appear(i, false);


            count = 0;
            Button[10].SetActive(false);
            temp = Dif;
            Check_Ans();
        }
    }
 
    public void Solving_Problems()
    {
        for (int i = 0; i < 3; i++)
            Button[i].SetActive(false);

        Button[15].SetActive(false);
        Button[16].SetActive(false);
        Button[7].SetActive(true);
        Button[8].SetActive(true);
        Cam.transform.LookAt(new Vector3(2.527f, 0, 0.267f));
        
    }
}
