using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	const string VERSION = "v0.0.1";
	public string roomName = "VVR";
	public string playerPrefabName = "Ninja";
	public Transform spawnPoint;

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings(VERSION);
	}

	void OnJoinedLobby()
	{
		RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 4 };
		PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
	}

	void OnJoinedRoom()
	{
		GameObject player = PhotonNetwork.Instantiate(playerPrefabName, spawnPoint.position, spawnPoint.rotation, 0);
		GameManager manager = this.GetComponent<GameManager>();
		manager.player1 = player.transform;
		manager.stats1 = player.GetComponent<NinjaControllerScript> ();

	}

}
