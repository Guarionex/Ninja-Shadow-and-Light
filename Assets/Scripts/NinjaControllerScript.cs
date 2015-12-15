using UnityEngine;
using System.Collections;

public class NinjaControllerScript : MonoBehaviour {


	public float maxSpeed = 10f;
	private bool facingRight = true;
	//public bool BlackNinja = true;

	Animator anim;

	bool grounded = false;
	public Transform groundCheck;
	float grounRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 700f;

	bool attack = false;
	public bool hit = false;
	public Transform sword;
	float swordRadius = 1f;
	public LayerMask whatIsPlayer;

	public int lifes = 3;
	int prevLife = 3;

	public bool hitDirection = true;

	public bool pause = false;

	public bool isControllable = false;
	public bool isMovingHorizontal = false;
	public bool isMovingHorizontalPhoton = false;
	public bool isSwordSwing = false;
	public bool isJumping = false;

	// Use this for initialization
	void Start () {
	
		anim = GetComponent<Animator> ();
	}
	

	void FixedUpdate ()
	{

		if(!pause)
		{
			attack = Physics2D.OverlapCircle (sword.position, swordRadius, whatIsPlayer);

			grounded = Physics2D.OverlapCircle (groundCheck.position, grounRadius, whatIsGround);
			anim.SetBool ("Ground", grounded);

			anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
			float move = 0f;
			if (isControllable) 
			{
				move = Input.GetAxis ("Horizontal");
			}
			if(move != 0) isMovingHorizontal = true;
			else isMovingHorizontal = false;
			anim.SetFloat ("Speed", isMovingHorizontal || isMovingHorizontalPhoton ? 1 : 0);
			if (lifes > 0) 
			{
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			}

			if (move > 0 && !facingRight) {
				Flip ();
			} else if (move < 0 && facingRight)
			{
				Flip ();
			}

			if (prevLife != lifes) 
			{
				anim.SetTrigger("Hurt");
				prevLife = lifes;
				anim.SetInteger("Lifes", lifes);
				if(hitDirection)
				{
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (10 * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);
				}
				else if(!hitDirection)
				{
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (-10 * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);
				}

			}
			if (lifes <= 0)
			{
				anim.SetTrigger("Death");
			}

		}
	}

	void Update()
	{


		if(!pause)
		{
			if (lifes > 0) 
			{

				if (grounded && Input.GetKeyDown (KeyCode.W) && isControllable) {
					anim.SetBool ("Ground", false);
					isJumping = true;
					GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
					
				}

				if (Input.GetKeyDown (KeyCode.Space) && isControllable) {
					//anim.SetTrigger ("Attack");
					Debug.Log("Attack ordered");
					isSwordSwing = true;
					if (attack && !hit) {
						hit = true;
					}
				}
			}
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void swingSword()
	{
		Debug.Log ("Made it to sword method");
		//if (isSwordSwing) {
			Debug.Log("Swinging Sword");
			anim.SetTrigger ("Attack");
			//isSwordSwing = false;
		//
	}

	public void notGrounded()
	{
		if (isJumping) {
			anim.SetBool ("Ground", false);
			//isJumping = false;
		}
	}
}
