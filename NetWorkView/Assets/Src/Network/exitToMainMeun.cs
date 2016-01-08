using UnityEngine;
using System.Collections;

public class exitToMainMeun : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(Network.peerType==NetworkPeerType.Server){
				GetComponent<NetworkView>().RPC("networkLoadlevel", RPCMode.All, "connect");
			}
			else{
				Network.Disconnect();
				Application.LoadLevel(0);
			}
		}
	}
}
