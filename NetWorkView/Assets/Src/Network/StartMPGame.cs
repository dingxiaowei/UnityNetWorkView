using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartMPGame : MonoBehaviour {

	
	// Update is called once per frame
	public void Click () {
		if(Network.peerType== NetworkPeerType.Server){
			//Application.LoadLevel("Multi_Level0");
			GetComponent<NetworkView>().RPC("networkLoadlevel", RPCMode.All, "Multi_Level0");
		}
	}
	[RPC]
	public void networkLoadlevel(string levelName){
		Application.LoadLevel(levelName);
	}
}
