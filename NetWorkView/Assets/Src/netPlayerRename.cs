using UnityEngine;
using System.Collections;

public class netPlayerRename : MonoBehaviour {
	void OnNetworkInstantiate(NetworkMessageInfo info){
		
		if(GetComponent<NetworkView>().isMine){
			Debug.Log(" is mine");
			//向玩家填充控制的角色
			netPlayerController.Player=gameObject;
			gameObject.GetComponent<Renderer>().material.color=Color.green;
			
		}
		else{
			//Destroy(gameObject.GetComponent<NavMeshAgent>());
			Debug.Log(" not mine");
		}
	
	}

}
