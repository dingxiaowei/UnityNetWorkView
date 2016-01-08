using UnityEngine;
using System.Collections;

public class netPlayerController : MonoBehaviour {
	public static GameObject Player;
	private NavMeshAgent navAgent;
	private Ray ray;
	private RaycastHit hit;
	void Start () {
		navAgent=Player.GetComponent<NavMeshAgent>();
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Mouse0)){
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)){
				requestMoveToServer(hit.point);
				Debug.DrawLine(GameObject.Find("Main Camera").transform.position,hit.point);
			}
		}
		
	}
	//向服务器发送移动信息
	private void requestMoveToServer(Vector3 pos){
		if(Network.peerType == NetworkPeerType.Server){
			navAgent.SetDestination(pos);
		}
		else{
			Debug.Log("send pos message"+pos+"  "+Player.GetComponent<NetworkView>().viewID);
			GetComponent<NetworkView>().RPC ("RequestMove",RPCMode.All,pos,Player.GetComponent<NetworkView>().viewID);
			
		}
		

	}
	//接受服务器端返回的位置信息
	[RPC]
	public void OnSetposition(Vector3 pos, NetworkViewID id){
		if(id==Player.GetComponent<NetworkView>().viewID){
			Player.GetComponent<NavMeshAgent>().SetDestination(pos);
			Debug.Log("receved pos message");
		}
			
	}
}
