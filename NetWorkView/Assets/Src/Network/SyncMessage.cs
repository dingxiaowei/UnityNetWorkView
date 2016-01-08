using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SyncMessage : MonoBehaviour {
	public Text inputField;
	public Text messageBox;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void Click () {
		Debug.Log("Start send");
		GetComponent<NetworkView>().RPC("updateMessage", RPCMode.All, inputField.text);
		
	}
	[RPC]
    void updateMessage(string message)
    {
		if(message!="")
			messageBox.text+=message+"\n";
       Debug.Log("has receved message");
    }
}
