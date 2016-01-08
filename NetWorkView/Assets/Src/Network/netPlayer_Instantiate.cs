using UnityEngine;
using System.Collections;

public class netPlayer_Instantiate : MonoBehaviour {
	public Transform repownPoss;
	public GameObject netPlayer;
	private bool excPlayer=false;
	// Use this for initialization
	void Awake () {
		
		if(!excPlayer){
			Network.Instantiate(netPlayer,repownPoss.position,repownPoss.rotation,0);
		}
	}
}
