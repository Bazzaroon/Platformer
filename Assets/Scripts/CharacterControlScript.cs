using UnityEngine;
using System.Collections;

public class CharacterControlScript : MonoBehaviour {

	public float Speed;
	public float VerticalSpeed;
	private Animator anim;
	public Rigidbody2D rb2d;
	public Camera mainCamera;
	private BoxCollider2D collider;
	private CircleCollider2D grounder;

	private bool jumping = false;

	private bool facingForward = true;


	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		collider = mainCamera.GetComponent<BoxCollider2D> ();
		grounder = GetComponentInChildren<CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

				var vRate = Input.GetAxis ("Vertical");

				if (jumping)
						vRate = 0;

				var rate = Input.GetAxis ("Horizontal");
				var tScale = transform.localScale;
				var tForm = transform.position;
				rb2d.velocity = new Vector2 (rate * Speed, vRate * VerticalSpeed);

				if (rate < 0 && facingForward) {
						transform.localScale = new Vector3 (-tScale.x, tScale.y);
						facingForward = false;
				} else if (rate > 0 && !facingForward) {
						transform.localScale = new Vector3 (Mathf.Abs (tScale.x), tScale.y);
						facingForward = true;
				}


				if (rate > 0 || rate < 0) {
						anim.SetBool ("Run", true);
				} else if (rate == 0) {
						anim.SetBool ("Run", false);
				}
		if (transform.position.y > 1) 
		{
			jumping = true;
		}

		if( Input.GetKeyDown( KeyCode.Space ) )
			Debug.Log( "Space key was pressed." );
	}



	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject.tag == "grasstag")
		{
			jumping = false;
		}
	}
}