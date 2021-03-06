﻿using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour {

	//public GameObject myCamera;
	bool isAlive = true;
	Vector3 position;
	Quaternion rotation;
	float lerpSmoothing = 10f;
	NinjaControllerScript controller;
	bool moving = false;

	// Use this for initialization
	void Start () {
		if(photonView.isMine)
		{
			//myCamera = GameObject.Find("../My Camera");
			//myCamera.SetActive(true);
			controller = GetComponent<NinjaControllerScript>();
			//controller.enabled = true;
		}
		else 
		{
			controller = GetComponent<NinjaControllerScript>();
			StartCoroutine("Alive");
		}

	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.isWriting && controller != null)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(controller.isMovingHorizontal);

		}
		else if(controller !=null)
		{
			position = (Vector3)stream.ReceiveNext();
			rotation = (Quaternion)stream.ReceiveNext();
			moving = (bool) stream.ReceiveNext();

		}
	}

	IEnumerator Alive()
	{
		while(isAlive && controller != null)
		{
			transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * lerpSmoothing); 
			transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * lerpSmoothing);
			controller.isMovingHorizontalPhoton = moving;

			yield return null;
		}
	}
	

}
