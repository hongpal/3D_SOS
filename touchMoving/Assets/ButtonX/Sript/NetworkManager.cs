using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    public GameObject Joystick;
    public GameObject Jenga;
    public GameObject Net_Code;
    public GameObject Ready_Str;
    public GameObject time_cont;
    public GameObject[] Button = new GameObject[13];
    public GameObject Cam;
    public Vector3[] v_block = new Vector3[30];
    public static GameObject[] Block = new GameObject[16 * 4];
    public  static int[] Ans = new int[16];
    public static int Dif;
    public static int turn; // 0 서버 1 클라이언트
    public static int problem = 0;
    public static float time;
    public static bool answer = false;  // 정답 오답
    public static bool Get_Answer = false;
    public static bool is_Re_Game = false;
    public static bool my_turn = false;
    public  TouchScreenKeyboard keyboard = null;
    private bool is_server_join = false;
    private bool is_client_join = false;
    public bool ready_check = false;
    private bool check = true;
    private int num;
    private static int count = 0;
    private string typeName = "";
    private string Server_Code = "";
    private HostData[] hostList;
    private const string gameName = "3D_SOS";
    private Text Ready_text;
    public static bool is_Ans = false;
    public bool notRecording = true;
    public bool sending = false;
    private AudioSource audioSource;
    int lastSample = 0;
    public string microphone;
    private List<string> allMicrophones = new List<string>();
    public AudioClip sendingClip;

    public void Start()
    {
        Ready_text = Ready_Str.GetComponent<Text>();

        audioSource = GetComponent<AudioSource>();

        //사용가능한 마이크들 찾기
        for (int i = 0; i < Microphone.devices.Length; i++)
        {
            if (microphone == null)//첫번째 마이크를 기본마이크로 설정
            {
                microphone = Microphone.devices[i];
            }
            allMicrophones.Add(Microphone.devices[i]);
        }
    }

    /* void FixedUpdate()
     {
         // If there is a connection
         if (Network.connections.Length > 0)
         {
             if (notRecording)
             {
                 notRecording = false;
                 sendingClip = Microphone.Start(null, true, 100, 44100);
                 sending = true;
             }
             else if (sending)
             {
                 int pos = Microphone.GetPosition(null);
                 int diff = pos - lastSample;

                 if (diff > 0)
                 {
                     float[] samples = new float[diff * sendingClip.channels];
                     sendingClip.GetData(samples, lastSample);
                     byte[] ba = ToByteArray(samples);
                     if (Network.isClient)
                         GetComponent<NetworkView>().RPC("Send", RPCMode.Server, ba, sendingClip.channels);
                     else
                         GetComponent<NetworkView>().RPC("Send", RPCMode.Others, ba, sendingClip.channels);
                 }
                 lastSample = pos;
             }
         }
     }

     [RPC]
     public void Send(byte[] ba, int chan)
     {
         if (Network.isClient)
             print("server-> client");
         else
             print("client-> server");
         float[] f = ToFloatArray(ba);
         audioSource.clip = AudioClip.Create(microphone, f.Length, chan, 44100, false);
         audioSource.clip.SetData(f, 0);
         if (!audioSource.isPlaying) audioSource.Play();

     }
     // Used to convert the audio clip float array to bytes
     public byte[] ToByteArray(float[] floatArray)
     {
         int len = floatArray.Length * 4;
         byte[] byteArray = new byte[len];
         int pos = 0;
         foreach (float f in floatArray)
         {
             byte[] data = System.BitConverter.GetBytes(f);
             System.Array.Copy(data, 0, byteArray, pos, 4);
             pos += 4;
         }
         return byteArray;
     }
     // Used to convert the byte array to float array for the audio clip
     public float[] ToFloatArray(byte[] byteArray)
     {
         int len = byteArray.Length / 4;
         float[] floatArray = new float[len];
         for (int i = 0; i < byteArray.Length; i += 4)
         {
             floatArray[i / 4] = System.BitConverter.ToSingle(byteArray, i);
         }
         return floatArray;
     }*/

    public void StartServer()
    {
        num = UnityEngine.Random.Range(0, 9999);
        typeName = num.ToString();
        Network.InitializeServer(2, 3300, !Network.HavePublicAddress());
        MasterServer.RegisterHost(typeName, gameName);
        Net_Code.SetActive(true);
        Text text = Net_Code.GetComponent<Text>();
        text.text = "Net Code : " + typeName;
        Button[0].SetActive(true);
        
    }

    public void init()
    {
        Ready_Str.SetActive(false);
        Net_Code.SetActive(false);
        ready_check = is_Re_Game = answer = Get_Answer = is_server_join = is_Ans = is_client_join = false;
        count = 0;
        Ready_text.text = "No Ready";
        GameObject.Find("Sin-2").GetComponent<Button_Event2>().init();
        
    }

    [RPC] public void UnConnect()
    {
        Network.Disconnect();
        Button[0].SetActive(false);
        Net_Code.SetActive(false);
        GameObject.Find("Menu").GetComponent<Menu_Event>().On_Off(0);
        zoomInAndOut.ok = false;
        gyroScope.ok = true;
        Destroy(ColiEvent.jenga);
        Joystick.SetActive(false);
        Cam.transform.position = new Vector3(0, 0, 0);
        Cam.transform.LookAt(new Vector3(0, 0, 5));
        for (int i = 0; i < 13; i++)
            Button[i].SetActive(false);
        for(int i = 0; i < 30; i ++)
        {
            if (genga.block[i] != null)
                Destroy(genga.block[i]);
        }

        init();
    }

    public void temp()
    {
        Button[0].SetActive(false);
        Net_Code.SetActive(false);
        GameObject.Find("Menu").GetComponent<Menu_Event>().On_Off(0);
        zoomInAndOut.ok = false;
        gyroScope.ok = true;
        Destroy(ColiEvent.jenga);
        Joystick.SetActive(false);
        Cam.transform.position = new Vector3(0, 0, 0);
        Cam.transform.LookAt(new Vector3(0, 0, 5));
        for (int i = 0; i < 13; i++)
            Button[i].SetActive(false);
        for (int i = 0; i < 30; i++)
        {
            if (genga.block[i] != null)
                Destroy(genga.block[i]);
        }

        init();
    }

    public void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        count = 0;
        if(Network.isClient)
        {
            temp();
            GetComponent<NetworkView>().RPC("UnConnect", RPCMode.Server);
        }
        else if(Network.isServer)
        {
            print("server");
            temp();
            GetComponent<NetworkView>().RPC("UnConnect", RPCMode.Others);
        }
        for (int i = 0; i < 10; i++)
            Button[i].SetActive(false);

        GameObject.Find("Sin-2").GetComponent<Button_Event2>().On_Off(1);

    }

    void OnServerInitialized()
    {
        count = 0;
        Debug.Log(typeName + " Server Initializied");
    }

    void OnGUI()
    {
        if (TouchScreenKeyboard.isSupported)
        {
            if (is_Ans)
            {
                Server_Code = "";

                keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumbersAndPunctuation, false, false, false, false, "Net Code");
                keyboard.text = "";
                is_Ans = false;

            }
            
            if (keyboard != null && keyboard.done)
            {
                if(keyboard.text.Equals(""))
                {
                    GameObject.Find("Sin-2").GetComponent<Button_Event2>().On_Off(5);
                    return;
                }
                Server_Code = keyboard.text;
                keyboard = null;

                RefreshHostList(Server_Code);
                
            }

            if (is_server_join)
            {
                for (int i = 0; i < hostList.Length; i++)
                {
                    if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
                        JoinServer(hostList[i]);
                }
            }
        }
    }

    private void RefreshHostList(string temp)
    {
        MasterServer.RequestHostList(temp);
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {

        if (msEvent == MasterServerEvent.HostListReceived)
        {
            hostList = MasterServer.PollHostList();

            if (hostList.Length == 0)
            {
                GameObject.Find("Sin-2").GetComponent<Button_Event2>().On_Off(5);
                return;
            }
            is_server_join = true;
        }

        else if(msEvent == MasterServerEvent.RegistrationSucceeded)
        {
            if (is_client_join)
            {
                Net_Code.SetActive(false);
                Button[1].SetActive(true);
                is_client_join = false;
                Ready_Str.SetActive(true);
            }
        } 
    }

    private void JoinServer(HostData hostData)
    {
        NetworkConnectionError e = Network.Connect(hostData);
        print(e);
    }
    
    void OnPlayerConnected(NetworkPlayer player)
    {
        is_client_join = true;
    }

    public void Re_Game_Ready()
    {
        GetComponent<NetworkView>().RPC("Re_Game_Start", RPCMode.All);
    }

    public void Ready()
    {
        if (Network.isClient)
        {
            if (!ready_check)
            {
                ready_check = true;
                Ready_text.text = "Ready!!";
                GetComponent<NetworkView>().RPC("Ready_Count", RPCMode.Server, ready_check);
            }

            else
            {
                ready_check = false;
                Ready_text.text = "No Ready";
                GetComponent<NetworkView>().RPC("Ready_Count", RPCMode.Server, ready_check);
            }
        }
        else
        {
            if (!ready_check)
            {
                ready_check = true;
                Ready_text.text = "Ready!!";
                Ready_Count(ready_check);
            }

            else
            {
                ready_check = false;
                Ready_text.text = "No Ready";
                Ready_Count(ready_check);
            }
        }
    }

    public void Result()
    {
        if (Network.isClient)
            GetComponent<NetworkView>().RPC("Ans_Check", RPCMode.All);
        else
            Ans_Check();
    }

    void OnConnectedToServer()
    {
        is_server_join = false;
        Button[1].SetActive(true);
        Ready_Str.SetActive(true);
        Debug.Log("Server Joined");   
    }

    public void Re_Game_Event(int check)
    {
        if(check == 1)  // 리 게임
        {
            Button_Event2.net_check = 1;
            if (Network.isClient)
            {
                GetComponent<NetworkView>().RPC("Re_Game", RPCMode.Server, check);

            }
            else
                Re_Game(check);
        }

        else if (check == -1)
        {
            if (Network.isClient)
            {
                GetComponent<NetworkView>().RPC("Re_Game", RPCMode.Server, check);

            }
            else
                Re_Game(check);
        }
    }

    [RPC] void Re_Game_Start()
    {
        ready_check = false;

        if (Network.isServer)
        {
            Button[1].SetActive(true);
            Ready_Str.SetActive(true);
        }

        else
        {
            Button[1].SetActive(true);
            Ready_Str.SetActive(true);
            Button[9].SetActive(false);
        }
    }

    [RPC] void Re_Game(int check)
    {
        if(check == -1) // 아직 확정 아님
        {
            count = 0;
            UnConnect();
        }

        else if(check == 1)
        {
            count += check;

            if(count == Network.connections.Length + 1)
            {
                is_Re_Game = true;
                count = 0;
                if (problem != 3)
                    GetComponent<NetworkView>().RPC("settings", RPCMode.All);
                else
                    GetComponent<NetworkView>().RPC("jenga_restart", RPCMode.All);
            }
        }

    }

    [RPC] public void jenga_restart()
    {
        Destroy(ColiEvent.jenga);
        print("asd");
        //ColiEvent.jenga.SetActive(true);
        Jenga.SetActive(true);
        Button[3].SetActive(false);
        Button[4].SetActive(false);
        Button[7].SetActive(false);
        Button[8].SetActive(false);
        /*for (int i = 0; i < 30; i++)
        {
            genga.block[i].SetActive(true);
            genga.block[i].transform.position = genga.block_vector[i];
            genga.block[i].transform.rotation = genga.block_rotate[i];
            genga.block[i].GetComponent<MeshRenderer>().material.color = TouchEvent.c;
        }*/
        if(Network.isServer)
        {
            Network.Instantiate(Jenga, Jenga.transform.position, Quaternion.identity, 0);
        }
    }

    [RPC] void settings()
    {
        for (int i = 3; i < 9; i++)
            Button[i].SetActive(false);

        if (Network.isServer)
        {
            GameObject.Find("Sin-2").GetComponent<Button_Event2>().Solving_Problems();
        }

        else
        {
            Button[9].SetActive(true);
        }
    }

    public void Game_Start()
    {
        if (Network.isClient)
        {
            Button[6].SetActive(false);
        }

        else
        {
            Button[6].SetActive(false);

            if (problem == 3)
            {
                Cam.transform.position = new Vector3(0, 0, 0);
                Cam.transform.LookAt(new Vector3(0, 0, 5f));
                Jenga.SetActive(true);
                Joystick.SetActive(true);
                Network.Instantiate(Jenga, Jenga.transform.position, Quaternion.identity, 0);
                Button[12].SetActive(true);
                Text text = Button[12].GetComponent<Text>();

                if (my_turn)
                    text.text = "My turn";
                else
                    text.text = "Wait";

                GetComponent<NetworkView>().RPC("Intent", RPCMode.Others, problem);
            }
            else
            {
                GetComponent<NetworkView>().RPC("Intent", RPCMode.Others, Ans, Dif, problem);
                GameObject.Find("Sin-2").GetComponent<Button_Event2>().CreateBlock();
            }
        }
    }

    [RPC] void Intent(int Server_problem)
    {
        problem = Server_problem;
        Button[12].SetActive(true);

        Text text = Button[12].GetComponent<Text>();

        if (my_turn)
            text.text = "My turn";
        else
            text.text = "Wait";

        Joystick.SetActive(true);
        Cam.transform.position = new Vector3(0, 0, 0);
        Cam.transform.LookAt(new Vector3(0, 0, 5f));
    }
    [RPC] void Intent(int[] Server_Ans, int Server_Dif, int Server_problem) // 블록이랑, 난이도만 넘어옴
    {
        Ans = Server_Ans;
        Dif = Server_Dif;
        problem = Server_problem;
        count = 0;
        Ready_text.text = "No Ready";
        Button[1].SetActive(false);
        Ready_Str.SetActive(false);
        GameObject.Find("Sin-2").GetComponent<Button_Event2>().CreateBlock(Ans, Dif, problem);

    }

    [RPC] void Ans_Check()
    {
        count++;
        print("Ans_Check : " + count );
        if(count == Network.connections.Length + 1)
        {
            GetComponent<NetworkView>().RPC("Count_Init", RPCMode.All);
        }

        else if(Get_Answer)
        {
            Button[2].SetActive(true);
        }


    }

    [RPC] void Result_Show(int select)   // 서버 : 0 클라이언트 : 1 비김 : 2
    {
        if(Network.isServer)
        {
            switch (select)
            {
                case 0:
                    Button[3].SetActive(true);
                    break;
                case 1:
                    Button[4].SetActive(true);
                    break;
                case 2:
                    Button[5].SetActive(true);
                    break;
            }
        }

        else
        {
            switch (select)
            {
                case 0:
                    Button[4].SetActive(true);
                    break;
                case 1:
                    Button[3].SetActive(true);
                    break;
                case 2:
                    Button[5].SetActive(true);
                    break;
            }
        }

        Button[7].SetActive(true);
        Button[8].SetActive(true);
        init();
    }

    [RPC] void Result_Check(bool Client_Ans, float Client_Time)
    {
        if (answer && Client_Ans)  // 시간 체크
        {
            if(time > Client_Time)  // 서버 승리
            {
                GetComponent<NetworkView>().RPC("Result_Show", RPCMode.All, 0);
            }

            else if(time < Client_Time) // 클라이언트 승리
            {
                GetComponent<NetworkView>().RPC("Result_Show", RPCMode.All, 1);
            }

            else if(time == Client_Time) // 비김
            {
                GetComponent<NetworkView>().RPC("Result_Show", RPCMode.All, 2);
            }
        }

        else if (!answer && Client_Ans) // 클라이언트 승리
        {
            GetComponent<NetworkView>().RPC("Result_Show", RPCMode.All, 1);
        }

        else if (answer && !Client_Ans) // 서버 승리
        {
            GetComponent<NetworkView>().RPC("Result_Show", RPCMode.All, 0);
        }

        else if(!answer && !Client_Ans) // 비김
        {
            GetComponent<NetworkView>().RPC("Result_Show", RPCMode.All, 2);
        }

    }

    [RPC] void Count_Init()
    {
        count = 0;
        Button[2].SetActive(false);
        print("cube_init");

        if (Network.isClient)
        {
            print("client");
            GetComponent<NetworkView>().RPC("Result_Check", RPCMode.Server, answer, time);
        }
        else
            print("server");

    }

    [RPC] void time_check()
    {
        Button[1].SetActive(false);
        Button[2].SetActive(false);
        Ready_Str.SetActive(false);
        Button[6].SetActive(true);
        count = 0;
    }

    [RPC] void Ready_Count(bool ready_check)
    {
        if(ready_check)
        {
            count++;
        }

        else
        {
            count--;
        }
        print("count" + count);       
        if (Network.isServer)
        {
            if (count == Network.connections.Length + 1)
            {
                Ready_text.text = "No Ready";
                count = 0;
                if(problem == 3)
                {
                    Button[1].SetActive(false);
                    Ready_Str.SetActive(false);
                    Button[10].SetActive(true);
                    Button[11].SetActive(true);
                    turn = UnityEngine.Random.Range(0, 2);
                    GetComponent<NetworkView>().RPC("turn_select", RPCMode.Others, turn);
                    return;
                }
                GetComponent<NetworkView>().RPC("time_check", RPCMode.All);
            }
        }
    }

    [RPC] public void turn_button_select(int turn_num)
    {
        Cam.transform.position = new Vector3(0, 0, 0);
        Cam.transform.LookAt(new Vector3(0, 0, 5f));

        switch(turn_num)
        {
            case 0:
                Button[11].SetActive(false);
                break;
            case 1:
                Button[10].SetActive(false);
                break;
        }
        Button[turn_num + 10].SetActive(false);
        Button[2].SetActive(true);

        if(turn_num == turn)
            my_turn = true;
            

        if (Network.isClient)
        {
            GetComponent<NetworkView>().RPC("jenga_start", RPCMode.Server, turn_num);
        }
        else
        {
            GetComponent<NetworkView>().RPC("jenga_start", RPCMode.All, turn_num);
        }
    }

    [RPC] void jenga_start(int turn_num)
    {
        count++;
        Button[turn_num + 10].SetActive(false);
        if (Network.isServer)
        {
            if (count == Network.connections.Length + 1)
            {
                GetComponent<NetworkView>().RPC("time_check", RPCMode.All);
            }
        }
    }

    [RPC] void turn_select(int num)
    {
        Button[1].SetActive(false);
        Ready_Str.SetActive(false);
        Button[10].SetActive(true);
        Button[11].SetActive(true);
        turn = num;
    }

    public void jenga_move(int num, Vector3 v)
    {
        GetComponent<NetworkView>().RPC("net_jenga_move", RPCMode.Server, num, v);
    }

    [RPC] void net_jenga_move(int num, Vector3 v)
    {
        genga.block[num].transform.position = v;
     }

    public void select(int num)
    {
        GetComponent<NetworkView>().RPC("net_select", RPCMode.Server, num);
    }

    [RPC]void net_select(int num)
    {
        JoyStick.Player = genga.block[num].transform;
        genga.block[num].GetComponent<MeshRenderer>().material.color = Color.black;
    }

    [RPC] public void time_go()
    {
        time_cont.SetActive(true);
    }

    [RPC] public void time_stop()
    {
        time_cont.SetActive(false);
    }

    [RPC] public void turn_change()
    {
        Text text = Button[12].GetComponent<Text>();

        if (!my_turn)
            text.text = "My turn";
        else
            text.text = "Wait";
        time_cont.SetActive(false);
        my_turn = !my_turn;
    }

    [RPC] public void jenga_result()
    {
        if (!my_turn)
            Button[3].SetActive(true);
        else
            Button[4].SetActive(true);

        Button[7].SetActive(true);
        Button[8].SetActive(true);
    }
}
