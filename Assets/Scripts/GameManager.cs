using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Transform player1;
	public Transform player2;
	public Transform background;
	bool hit1;
	bool hit2;
	NinjaControllerScript stats1;
	NinjaControllerScript stats2;
	BackgroundInverter backgroundInverter;

	// Use this for initialization
	void Start () {
		stats1 = player1.GetComponent<NinjaControllerScript> ();
		stats2 = player2.GetComponent<NinjaControllerScript> ();
		backgroundInverter = background.GetComponent < BackgroundInverter> ();

	}
	
	// Update is called once per frame
	void Update () {
	

		hit1 = stats1.hit;
		hit2 = stats2.hit;



		if (hit1) 
		{
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
}
