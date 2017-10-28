using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {

    public GameObject Net_Code;
    public GameObject Cam;
    public  static int[] Ans = new int[16];
    public static int Dif;
    private TouchScreenKeyboard keyboard = null;
    private bool is_join = false;
    private int num;
    private int count = 0;
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

        if (is_join)
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
                is_Ans = true;
                return;
            }
            is_join = true;
        }

        else if(msEvent == MasterServerEvent.RegistrationSucceeded)
        {
            count++;

            if (count == 2)
            {
                Net_Code.SetActive(false);
                //GameObject.Find("Sin-2").GetComponent<Button_Event2>().CreateBlock();
            }
        } 
    }

    private void JoinServer(HostData hostData)
    {
        NetworkConnectionError e = Network.Connect(hostData);

        
    }
    
    void OnPlayerConnected(NetworkPlayer player)
    {
        GetComponent<NetworkView>().RPC("test", RPCMode.Others, Ans, Dif);
        GameObject.Find("Sin-2").GetComponent<Button_Event2>().CreateBlock();
    }

    void OnConnectedToServer()
    {
        is_join = false;
        Debug.Log("Server Joined");
        print(Dif);
    }

    [RPC] void test(int[] temp, int test)
    {
        print("asd");
        Ans = temp;
        Dif = test;
        GameObject.Find("Sin-2").GetComponent<Button_Event2>().CreateBlock(Dif, Ans);
        
    }

}
