using UnityEngine;
using System.Collections;

public class netGameManager : MonoBehaviour {
	public static GameObject[] netPlayers;
	void Awake(){
		Network.sendRate = 120;
		Debug.Log(Network.peerType);
	}
	void Start(){
		netPlayers=GameObject.FindGameObjectsWithTag("Player");

	}
	void OnNetworkInstantiate(NetworkMessageInfo info){
		netPlayers=GameObject.FindGameObjectsWithTag("Player");

	}
	[RPC]
	//向客户端返回其请求的信息，如果自己是服务器端，则不返回信息。
	public void RequestMove(Vector3 pos,NetworkViewID id){
		netPlayers=GameObject.FindGameObjectsWithTag("Player");
		Debug.Log(netPlayers.Length);
		//networkView.RPC ("OnSetposition",RPCMode.All,pos,id);
		if(Network.peerType == NetworkPeerType.Server){
			for(int i=0;i<netPlayers.Length;i++){
				if(netPlayers[i].GetComponent<NetworkView>().viewID==id){
					netPlayers[i].GetComponent<NavMeshAgent>().SetDestination(pos);
					
				}
			}
		}
		else{
			GetComponent<NetworkView>().RPC ("OnSetposition",RPCMode.All,pos,id);
			Debug.Log("receved pos message"+pos+"  "+id);
		}
	}

	//玩家离开游戏时销毁对象
	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Clean up after player " + player);
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}
}
