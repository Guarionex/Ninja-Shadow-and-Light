using UnityEngine;
using System.Collections;

public class PlayerInstantiate : MonoBehaviour {

	public int numPlayer = 0;

	public Transform blackNinja;

	void OnJoinedRoom()
	{
		CreatePlayerObject();
		numPlayer++;
	}
	
	void CreatePlayerObject()
	{
		if (numPlayer == 1)
		{
			Vector3 position = new Vector3 (-10f, -6f, -.5f);
		
			Instantiate(blackNinja, position, Quaternion.identity);
			//GameObject newPlayerObject = PhotonNetwork.Instantiate ("Black Ninja", position, Quaternion.identity, 0);
		}

	}
}
