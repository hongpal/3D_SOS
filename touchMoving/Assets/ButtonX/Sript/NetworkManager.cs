using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {

    public GameObject Net_Code;
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
    public static bool is_Ans = false;

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
        Get_Answer = is_server_join = is_Ans = is_client_join = false;
        count = 0;
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
        
    }

    public void Ready()
    {
       if(!ready_check)
        {
            ready_check = true;
            GetComponent<NetworkView>().RPC("Ready_Count", RPCMode.All);
        }
        
       else
        {
            ready_check = false;
            GetComponent<NetworkView>().RPC("Ready_Count", RPCMode.All);
        }
    }

    public void Result()
    {
        GetComponent<NetworkView>().RPC("Ans_Check", RPCMode.All);
    }

    void OnConnectedToServer()
    {
        is_server_join = false;
        Button[1].SetActive(true);
        Debug.Log("Server Joined");   
    }

    [RPC] void Intent(int[] Server_Ans, int Server_Dif, int Server_problem) // 블록이랑, 난이도만 넘어옴
    {
        Ans = Server_Ans;
        Dif = Server_Dif;
        problem = Server_problem;
        count = 0;
        Button[1].SetActive(false);
        GameObject.Find("Sin-2").GetComponent<Button_Event2>().CreateBlock(Ans, Dif, problem);

    }

    [RPC] void Ans_Check()
    {
        count++;

        if(count == Network.connections.Length + 1)
        {
            
        }

        else if(Get_Answer)
        {
            Button[2].SetActive(true);
        }


    }

    [RPC] void Ready_Count()
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
                print(count);                count = 0;
                Button[1].SetActive(false);
                GetComponent<NetworkView>().RPC("Intent", RPCMode.Others, Ans, Dif, problem);
                GameObject.Find("Sin-2").GetComponent<Button_Event2>().CreateBlock();
            }
        }
    }
}
