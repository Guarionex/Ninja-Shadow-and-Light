using UnityEngine;
using System.Collections;

public class BackgroundInverter : MonoBehaviour {

	SpriteRenderer sprite;
	public Sprite background1;
	public Sprite background2;
	public bool flip = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		sprite = GetComponent<SpriteRenderer> ();
		if (flip) 
		{
			if (sprite.sprite.Equals (background1)) {
				sprite.sprite = background2;
			} else if (sprite.sprite.Equals (background2)) {
				sprite.sprite = background1;
			}
			flip = false;
		}
	}
}
