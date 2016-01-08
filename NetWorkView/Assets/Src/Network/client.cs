using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class client : MonoBehaviour {
	public Transform RespownPos;
	
	public Text ipString;
    private string IP = "";
    private string clientIp;
    private string clientIpSplite;
    private Vector3 acceleration;
    public GameObject cube;
	//Connet port 
    private int Port = 40001;
	private bool isOn;
    void Awake()
    {
		isOn=false;
        clientIp = Network.player.ipAddress;
        string[] tmpArray = clientIp.Split('.');
        clientIpSplite = tmpArray[0] + "." + tmpArray[1] + "." + tmpArray[2] + ".";
    }
    
	public void Click(){
		IP=ipString.text;
		isOn=true;
	}
	
    void Update()
    {
        switch (Network.peerType)
        {
            case NetworkPeerType.Disconnected:
                StartConnect();
                break;
            case NetworkPeerType.Server:
                break;
            case NetworkPeerType.Client:
                OnConnect();
                break;
            case NetworkPeerType.Connecting:
                break;
        }
    }


 
	public  void StartConnect()
    {
        if (isOn)
        {
            NetworkConnectionError error = Network.Connect(IP, Port);
            Debug.Log("connect status:" + error);   
			isOn=false;
        }
    }

    void OnConnect()
    {
		ipString.text="Connect sucssesful";
		//链接成功
		/*
        if(!cubeInitialed)
        {
            Network.Instantiate(cube, RespownPos.position, RespownPos.rotation, 0);
            cubeInitialed = true;
        }
        */
    }
}
