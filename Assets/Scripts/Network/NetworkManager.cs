using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	const string VERSION = "v0.0.1";
	public string roomName = "VVR";
	public string playerPrefabName = "Ninja";
	public string player2PrefabName = "Ninja";
	public Transform blackSpawnPoint;
	public Transform whiteSpawnPoint;
	private static PhotonView scenePhotonView;
	//public static int playerWhoIsIt = 0;

	public Transform player1;
	public Transform player2;
	public Transform spawnLocation1;
	public Transform spawnLocation2;
	public Transform background;
	bool hit1 = false;
	bool hit2 = false;
	public NinjaControllerScript stats1;
	NinjaControllerScript stats2;
	BackgroundInverter backgroundInverter;
	bool pause = false;
	Rect pauseWindowRect = new Rect(0, 0, Screen.width, Screen.height);
	Rect winWindowRect = new Rect(0, 0, Screen.width, Screen.height);


	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings(VERSION);
		scenePhotonView = this.GetComponent<PhotonView>();

		if(player1 != null)
		{
			stats1 = player1.GetComponent<NinjaControllerScript> ();
			
		}
		if (player2 != null) 
		{
			stats2 = player2.GetComponent<NinjaControllerScript> ();
		}
		backgroundInverter = background.GetComponent < BackgroundInverter> ();

	}

	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			pauseToggle(!pause);
			
			//Debug.Log("ESC button pressed, pause listener");
		}
		
		/*if(stats1.pause || stats2.pause)
		{
			pause = true;
		}*/
		
		/*if (player1 != null) {
			Debug.Log("Player 1 exist");
		}*/
		if (player2 != null) {
			Debug.Log("Player 2 exist");
		}
		if(player1 != null && player2 != null)
		{
			Debug.Log("Players are not null");
			hit1 = stats1.hit;
			hit2 = stats2.hit;
		}
		if (hit1) 
		{
			Debug.Log("Player 1 got a hit");
			stats2.lifes--;
			stats1.hit = false;
			backgroundInverter.flip = true;
			if(player2.position.x > player1.position.x) stats2.hitDirection = true;
			else if(player2.position.x < player1.position.x) stats2.hitDirection = false;
			
		}
		if (hit2) 
		{
			stats1.lifes--;
			stats2.hit = false;
			backgroundInverter.flip = true;
			if(player1.position.x > player2.position.x) stats1.hitDirection = true;
			else if(player1.position.x < player2.position.x) stats1.hitDirection = false;
		}
		
		
	}
	
	void OnGUI()
	{
		
		if(pause)
		{
			pauseWindowRect = GUI.Window(0, pauseWindowRect, PauseWindow, "Pause Menu");
		}
		if(player1 != null && player2 != null)
		{
			if(stats1.lifes <= 0 || stats2.lifes <= 0)
			{
				winWindowRect = GUI.Window(1, winWindowRect, WinWindow, (stats1.lifes <= 0) ? "White Ninja Wins" : "Black Ninja Wins");
			}
		}
	}
	
	void PauseWindow(int windowID) {
		if (GUI.Button(new Rect(pauseWindowRect.center.x - 50, pauseWindowRect.center.y - 10, 100, 20), "Resume"))
		{
			pauseToggle(false);
		}
		if(GUI.Button(new Rect(pauseWindowRect.center.x - 50, pauseWindowRect.center.y + 20, 100, 20), "Main Screen"))
		{
			pauseToggle(false);
			Application.LoadLevel (0);
		}
		if(GUI.Button(new Rect(pauseWindowRect.center.x - 50, pauseWindowRect.center.y + 50, 100, 20), "Restart"))
		{
			pauseToggle(false);
			restart();
			//Application.LoadLevel(1);
		}
		
	}
	
	void WinWindow(int windowID)
	{
		GUI.Label(new Rect(winWindowRect.center.x - 50, winWindowRect.center.y - 40, 100, 20), (stats1.lifes <= 0) ? "White Ninja Wins" : "Black Ninja Wins");
		if(GUI.Button(new Rect(winWindowRect.center.x - 50, winWindowRect.center.y - 10, 100, 20), "Rematch"))
		{
			restart();
			//Application.LoadLevel(1);
		}
		if(GUI.Button(new Rect(winWindowRect.center.x - 50, winWindowRect.center.y + 20, 100, 20), "Main Screen"))
		{
			Application.LoadLevel (0);
		}
	}
	
	void pauseToggle(bool toPause)
	{
		pause = toPause;
		if(pause) Time.timeScale = 0;
		else Time.timeScale = 1;
		
		stats1.pause = pause;
		stats2.pause = pause;
	}
	
	private void restart()
	{
		player1.transform.position = new Vector3(spawnLocation1.transform.position.x, spawnLocation1.transform.position.y, spawnLocation1.transform.position.z);
		stats1.lifes = 3;
		player2.transform.position = new Vector3(spawnLocation2.transform.position.x, spawnLocation2.transform.position.y, spawnLocation2.transform.position.z);
		stats2.lifes = 3;
	}



	void OnJoinedLobby()
	{
		RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 2 };
		PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
	}

	void OnJoinedRoom()
	{
		Debug.Log ("Number of players in room: " + PhotonNetwork.playerList.Length);
		if (PhotonNetwork.playerList.Length == 1) {
			GameObject blackNinja = PhotonNetwork.Instantiate (playerPrefabName, blackSpawnPoint.position, blackSpawnPoint.rotation, 0); 
			player1 = blackNinja.transform;
			stats1 = player1.GetComponent<NinjaControllerScript> ();
		} else if (PhotonNetwork.playerList.Length == 2) {
			Debug.Log("I'm in 2");
			GameObject whiteNinja = PhotonNetwork.Instantiate (player2PrefabName, whiteSpawnPoint.position, whiteSpawnPoint.rotation, 0); 
			player2 = whiteNinja.transform;
			stats2 = player2.GetComponent<NinjaControllerScript> ();
			GameObject blackNinja = GameObject.Find("Black Ninja(Clone)");
			player1 = blackNinja.transform;
			stats1 = player1.GetComponent<NinjaControllerScript>();
		}


		/*if (PhotonNetwork.playerList.Length == 1)
		{
			playerWhoIsIt = PhotonNetwork.player.ID;
		}
		
		Debug.Log("playerWhoIsIt: " + playerWhoIsIt);*/

	}

	void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		Debug.Log("On Photon Player Connected: " + player.ID);

		if (player.ID == 2) {
			//GameObject whitenNinja = GameObject.Find("White Ninja(Clone)");
			//player2 = whitenNinja.transform;
			//stats2 = player2.GetComponent<NinjaControllerScript>();
			StartCoroutine("findSecondPlayer");
		}

		if(PhotonNetwork.isMasterClient)
		{
			//TagPlayer(playerWhoIsIt);
		}
	}

	IEnumerator findSecondPlayer()
	{
		while(GameObject.Find("White Ninja(Clone)") == null)
		{
			Debug.Log("Haven't found it");
			yield return null;
		}
		if (GameObject.Find ("White Ninja(Clone)") != null) {
			GameObject whitenNinja = GameObject.Find("White Ninja(Clone)");
			player2 = whitenNinja.transform;
			stats2 = player2.GetComponent<NinjaControllerScript>();
			Debug.Log("Found it");
			yield return null;
		}
	}
	
	/*public static void TagPlayer(int playerID)
	{
		Debug.Log("Tag Player: "+ playerID);
		scenePhotonView.RPC("TaggedPlayer", PhotonTargets.All, playerID);
	}*/

	/*[PunRPC]
	public void TaggedPlayer(int playerID)
	{
		playerWhoIsIt = playerID;
		Debug.Log("TaggedPlayer: " + playerID);
	}*/

	public void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		Debug.Log("OnPhotonPlayerDisconnected: " + player);
		
		if (PhotonNetwork.isMasterClient)
		{
			/*if (player.ID == playerWhoIsIt)
			{
				// if the player who left was "it", the "master" is the new "it"
				TagPlayer(PhotonNetwork.player.ID);
			}*/
		}
	}

}
