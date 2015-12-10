using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour {

	//public GameObject myCamera;


	// Use this for initialization
	void Start () {
		if(photonView.isMine)
		{
			//myCamera = GameObject.Find("../My Camera");
			//myCamera.SetActive(true);
			GetComponent<NinjaControllerScript>().enabled = true;
		}
		else 
		{

		}
	}
	

}
