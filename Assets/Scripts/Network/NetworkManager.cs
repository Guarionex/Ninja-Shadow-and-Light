using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	const string VERSION = "v0.0.1";
	public string roomName = "VVR";
	public string playerPrefabName = "Ninja";
	public Transform blackSpawnPoint;
	public Transform whiteSpawnPoint;
	private static PhotonView scenePhotonView;
	public static int playerWhoIsIt = 0;


	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings(VERSION);
		scenePhotonView = this.GetComponent<PhotonView>();

	}



	void OnJoinedLobby()
	{
		RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 2 };
		PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
	}

	void OnJoinedRoom()
	{
		GameObject player = PhotonNetwork.Instantiate(playerPrefabName, blackSpawnPoint.position, blackSpawnPoint.rotation, 0);
		GameManager manager = this.GetComponent<GameManager>();
		manager.player1 = player.transform;
		manager.stats1 = player.GetComponent<NinjaControllerScript> ();
		manager.numberOfPlayers++;

		if (PhotonNetwork.playerList.Length == 1)
		{
			playerWhoIsIt = PhotonNetwork.player.ID;
		}
		
		Debug.Log("playerWhoIsIt: " + playerWhoIsIt);

	}

	void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		Debug.Log("On Photon Player Connected: " + player);
		
		if(PhotonNetwork.isMasterClient)
		{
			TagPlayer(playerWhoIsIt);
		}
	}
	
	public static void TagPlayer(int playerID)
	{
		Debug.Log("Tag Player: "+ playerID);
		scenePhotonView.RPC("TaggedPlayer", PhotonTargets.All, playerID);
	}

	[PunRPC]
	public void TaggedPlayer(int playerID)
	{
		playerWhoIsIt = playerID;
		Debug.Log("TaggedPlayer: " + playerID);
	}

	public void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		Debug.Log("OnPhotonPlayerDisconnected: " + player);
		
		if (PhotonNetwork.isMasterClient)
		{
			if (player.ID == playerWhoIsIt)
			{
				// if the player who left was "it", the "master" is the new "it"
				TagPlayer(PhotonNetwork.player.ID);
			}
		}
	}

}
