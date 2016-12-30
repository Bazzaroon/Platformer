using UnityEngine;
using System.Collections;

public class CharacterControlScript : MonoBehaviour {

	public float Speed;
	public float VerticalSpeed;
	public Rigidbody2D rb2d;
	public Camera mainCamera;
	public float JumpHeight;
	public float jumpForwardSpeed;
	public float JumpSpeed;

	private Animator anim;
	private bool jumping = false;
	private bool facingForward = true;
	private float jumpStart;
	private BoxCollider2D collider;
	private CircleCollider2D grounder;
	private float drag = 0f;



	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		collider = mainCamera.GetComponent<BoxCollider2D> ();
		grounder = GetComponentInChildren<CircleCollider2D> ();
		//Position player in center of screen
		transform.position = new Vector2 (0f, transform.position.y);
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		        
		if (jumping)
		{
			rb2d.AddForce(new Vector2(0,JumpSpeed));
			rb2d.drag = drag += 2;
			anim.SetBool("Jump", true);
		}

		var rate = Input.GetAxis ("Horizontal");
		var tScale = transform.localScale;
		var tForm = transform.position;
		var cameraPos = mainCamera.transform.position;

		mainCamera.transform.position  = new Vector3 (cameraPos.x , 0, 0);
		//rb2d.velocity = new Vector2 (rate * Speed, 0);

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
				
		if (transform.position.y > jumpStart + JumpHeight) 
		{
			drag = 0;		
			jumping = false;
		}

	}

	void Update()
	{
		mainCamera.transform.Translate(new Vector3(-transform.position.x, mainCamera.transform.position.y, 0));
		if (Input.GetKeyDown (KeyCode.Space))
		{
			jumping = true;
		}

	}


	void OnCollisionEnter2D(Collision2D coll)
	{
		jumping = false;
		anim.SetBool("Jump", false);
		drag = 0;
	}
}