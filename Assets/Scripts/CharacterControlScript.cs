using UnityEngine;
using System.Collections;

public class CharacterControlScript : MonoBehaviour {

	public float Speed;
	public float VerticalSpeed;
	public Rigidbody2D rb2d;
	public Camera mainCamera;
	public float JumpHeight;
	public float jumpForwardSpeed;

	private Animator anim;
	private bool jumping = false;
	private bool facingForward = true;
	private float jumpStart;
	private BoxCollider2D collider;
	private CircleCollider2D grounder;



	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		collider = mainCamera.GetComponent<BoxCollider2D> ();
		grounder = GetComponentInChildren<CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {


				var vRate = jumping ? 1f : 0f;
		        
				if (jumping) {
						VerticalSpeed = VerticalSpeed / ((JumpHeight + jumpStart) + Mathf.Abs (jumpStart) / Mathf.Abs (transform.position.y));
				}
		//Debug.Log ("Jump Height:" + JumpHeight.ToString() + " Jump Start:" + jumpStart.ToString());

				VerticalSpeed = VerticalSpeed < 1 ? 1 : VerticalSpeed;

				var rate = jumping ? jumpForwardSpeed : Input.GetAxis ("Horizontal");
				var tScale = transform.localScale;
				var tForm = transform.position;
				rb2d.velocity = new Vector2 (rate * Speed, vRate * VerticalSpeed);

				if (rate < 0 && facingForward) {
						transform.localScale = new Vector3 (-tScale.x, tScale.y);
						jumpForwardSpeed = Mathf.Abs(jumpForwardSpeed);
						facingForward = false;
				} else if (rate > 0 && !facingForward) {
						transform.localScale = new Vector3 (Mathf.Abs (tScale.x), tScale.y);
						//jumpForwardSpeed = -jumpForwardSpeed;
						facingForward = true;
				}


				if (rate > 0 || rate < 0) {
						anim.SetBool ("Run", true);
				} else if (rate == 0) {
						anim.SetBool ("Run", false);
				}
				if (transform.position.y > jumpStart + JumpHeight) {
					rb2d.gravityScale = 5;
					jumping = false;
				}

		if (Input.GetKeyDown (KeyCode.Space))
		{
			jumpStart = transform.position.y;
			jumping = true;
		}
	}



	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject.tag == "grasstag")
		{
			VerticalSpeed = 20f;
			jumping = false;
		}
	}
}