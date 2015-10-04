using UnityEngine;
using System.Collections;

public class SwordCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onTriggerEnter(Collider other)
	{
		Debug.Log ("Other Trigger");
	}

	void onTriggerEnter2D(Collider2D other)
	{
		Debug.Log ("Sword hit");
	}

	void onTriggerExit2D(Collider2D other)
	{
		Debug.Log ("Sword hit exit");
	}

	void onTriggerStay2D(Collider2D other)
	{
		Debug.Log ("Sword hit stay");
	}
}
