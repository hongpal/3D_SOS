using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {

    public GameObject Net_Code;
    public GameObject Ready_Str;
    public GameObject[] Button = new GameObject[7];
    public GameObject Cam;
    public static GameObject[] Block = new GameObject[16 * 4];
    public  static int[] Ans = new int[16];
    public static int Dif;
    public static int problem = 0;
    public static float time;
    public static bool answer = false;  // 정답 오답
    public static bool Get_Answer = false;
    public  TouchScreenKeyboard keyboard = null;
    private bool is_server_join = false;
    private bool is_client_join = false;
    private bool ready_check = false;
    private bool check = true;
    private int num;
    private static int count = 0;
    private string typeName = "";
    private string Server_Code = "";
    private HostData[] hostList;
    private const string gameName = "3D_SOS";
    private Text Ready_text;
    public static bool is_Ans = false;

    public void Start()
    {
        Ready_text = Ready_Str.GetComponent<Text>();
    }

    public void StartServer()
    {
        num = Random.Range(0, 9999);
        typeName = num.ToString();
        Network.InitializeServer(2, 5500, !Network.HavePublicAddress());
        MasterServer.RegisterHost(typeName, gameName);
        Net_Code.SetActive(true);
        Text text = Net_Code.GetComponent<Text>();
        text.text = "Net Cdoe : " + typeName;
        Button[0].SetActive(true);
    }

    public void init()
    {
        answer = Get_Answer = is_server_join = is_Ans = is_client_join = false;
        count = 0;
        Ready_text.text = "NO Ready";
        GameObject.Find("Sin-2").GetComponent<Button_Event2>().init();
    }

    public void UnConnect()
    {
        Network.Disconnect();
        Button[0].SetActive(false);
        Net_Code.SetActive(false);
        GameObject.Find("Menu").GetComponent<Menu_Event>().On_Off(0);
        zoomInAndOut.ok = false;
        gyroScope.ok = true;
        Cam.transform.position = new Vector3(0, 0, 0);
        Cam.transform.LookAt(new Vector3(0, 0, 5));
        init();
    }

    void OnServerInitialized()
    {
        Debug.Log(typeName + " Server Initializied");
    }

    void OnGUI()
    {
        if (is_Ans)
        {
            Server_Code = "";

            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumbersAndPunctuation, false, false, false, false);
            
            is_Ans = false;

        }

        if (keyboard != null && keyboard.done)
        {
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
    }
    
    void OnPlayerConnected(NetworkPlayer player)
    {
        is_client_join = true;
        Button[1].SetActive(true);
        Ready_Str.SetActive(true);
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
            GetComponent<NetworkView>().RPC("Ans_Check", RPCMode.Server);
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

    [RPC] void Intent(int[] Server_Ans, int Server_Dif, int Server_problem) // 블록이랑, 난이도만 넘어옴
    {
        Ans = Server_Ans;
        Dif = Server_Dif;
        problem = Server_problem;
        count = 0;
        Ready_text.text = "NO Ready";
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
       
        if (Network.isServer)
        {
            if (count == Network.connections.Length + 1)
            {
                count = 0;
                Button[1].SetActive(false);
                Ready_Str.SetActive(false);
                Ready_text.text = "NO Ready";
                GetComponent<NetworkView>().RPC("Intent", RPCMode.Others, Ans, Dif, problem);
                GameObject.Find("Sin-2").GetComponent<Button_Event2>().CreateBlock();
            }
        }
    }
}
