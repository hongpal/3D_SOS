using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {

    public GameObject Net_Code;
    private TouchScreenKeyboard keyboard = null;
    private int num;
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

        if (hostList != null)
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
        }

        else if(msEvent == MasterServerEvent.RegistrationSucceeded)
        {
            print("ok!!");
        }

        else if(msEvent == MasterServerEvent.RegistrationFailedNoServer)
        {
            is_Ans = true;
        }
        print("asd");
        print(msEvent);
    }

    private void JoinServer(HostData hostData)
    {
        print("server");
        Network.Connect(hostData);
    }

    void OnConnectedToServer()
    {
        Debug.Log("Server Joined");
    }



}
