using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class EnterScreen : MonoBehaviour {
	Button button;
	// Use this for initialization
	void Start () {
		button = GetComponent<Button> ();
		button.onClick.AddListener (LoadLevel);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Return)) 
		{
			LoadLevel();
		}
	}

	void LoadLevel()
	{
		Application.LoadLevel (1);
	}
}
