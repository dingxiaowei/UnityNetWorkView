using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class server : MonoBehaviour {
    private int serverPort; 
	private bool isOn;
	public Text serverInf;
	public Text me;
	public Text tex;

	public GameObject StartGame_bt;
    void Awake()
    {
		Network.sendRate = 60;
		isOn=false;
		serverInf.text="Server Offline";
        serverPort = 40001;
		if(Network.peerType== NetworkPeerType.Disconnected){
			//StartGame_bt.SetActive(false);
			me.text="Start Server";
		}
			
    }

	public void Click(){
		isOn=true;
	}

    void Update()
    {
		
		if(Network.peerType== NetworkPeerType.Server){
			tex.text="Start Game";		}
		else if(Network.peerType== NetworkPeerType.Client) {
			tex.text="Wait to start";
		}
		else if(Network.peerType== NetworkPeerType.Connecting){
			tex.text="Join...";
		}
		else
			tex.text="no connect";
		
        //Network.peerType是端类型的状态:  
        //即disconnected, connecting, server 或 client四种  
        switch (Network.peerType)
        {
            //禁止客户端连接运行, 服务器未初始化  
            case NetworkPeerType.Disconnected:
                StartServer();
                break;
            //运行于服务器端  
            case NetworkPeerType.Server:
                OnServer();
                break;
            //运行于客户端  
            case NetworkPeerType.Client:
                break;
            //正在尝试连接到服务器  
            case NetworkPeerType.Connecting:
                break;
        }
    }

    void StartServer()
    {
		
        //当用户点击按钮的时候为true  
        if (isOn)
        {
            //初始化本机服务器端口，第一个参数就是本机接收多少连接  
            NetworkConnectionError error = Network.InitializeServer(12, serverPort, false);
			
			me.text="End Server";
            Debug.Log("错误日志" + error);
			isOn=false;
        }
		serverInf.text="Server Offline";
    }

    void OnServer()
    {
		//Debug.Log("On server");
        //GUILayout.Label("服务端已经运行,等待客户端连接");  
        int length = Network.connections.Length;
		if(isOn){
			Network.Disconnect();
			
			me.text="Start Server";
			isOn=false;
		}
		serverInf.text="Server \n"+Network.player.ipAddress+" Online."+"\n"+"connected Client:"+length;
    }
	/// <summary>
	/// Raises the serialize network view event.
	/// </summary>
	/// <param name='stream'>
	/// Stream.
	/// </param>
	/// <param name='info'>
	/// Info.
	/// </param>/
   
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)

	{
    // Always send transform (depending on reliability of the network view) 
    
		if (stream.isWriting) 
    
		{
        
			Vector3 pos = transform.localPosition; 
        
			Quaternion rot = transform.localRotation; 
        
			stream.Serialize(ref pos); 
        
			stream.Serialize(ref rot); 
    
		}
    // When receiving, buffer the information 
    
		else {
        // Receive latest state information 
       
			Vector3 pos = Vector3.zero; 
        
			Quaternion rot = Quaternion.identity; 
        
			stream.Serialize(ref pos); 
       
			stream.Serialize(ref rot);
    
		}

	}
	
}
