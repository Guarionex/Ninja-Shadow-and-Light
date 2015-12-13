using UnityEngine;
using System.Collections;

public class NetworkGame : Photon.MonoBehaviour {

	private static PhotonView scenePhotonView;

	// Use this for initialization
	void Start () {
		scenePhotonView = this.GetComponent<PhotonView>();
	}
	
	void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		Debug.Log("On Photon Player Connected: " + player);

		if(PhotonNetwork.isMasterClient)
		{
			TagPlayer(5);
		}
	}

	public static void TagPlayer(int playerID)
	{
		Debug.Log("Tag Player: "+ playerID);
		scenePhotonView.RPC("TaggedPlayer", PhotonTargets.All, playerID);
	}
}
