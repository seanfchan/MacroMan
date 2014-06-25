using UnityEngine;
using System.Collections;

public class MacroManScript : MonoBehaviour {
	public float horizontalSpeed = 10.0f;
	public float jumpSpeed = 10.0f;

	private Vector2 velocity = new Vector2 (0.0f, 0.0f);
	private Animator animator;

	private bool jump = false;

	void Start () {
		animator = GetComponent<Animator> ();
	}

	void Update () {
		// movement
		float horizontalAxis = Input.GetAxis ("Horizontal");
		if (horizontalAxis > 0.0f) {
			animator.SetTrigger ("StartRunningRight");
			animator.SetBool ("IsRunning", true);
			velocity.x = horizontalSpeed;
		} else if (horizontalAxis < 0.0f) {
			animator.SetTrigger ("StartRunningLeft");
			animator.SetBool ("IsRunning", true);
			velocity.x = -horizontalSpeed;
		} else {
			animator.SetTrigger("StopRunning");
			animator.SetBool ("IsRunning", false);
			velocity.x = 0.0f;
		}

		float verticalAxis = Input.GetAxis ("Vertical");
		if (verticalAxis > 0.0f) {
			// climb up?
		} else if (verticalAxis < 0.0f) {
			// climb down?
		}

		// shooting
		if (Input.GetButtonDown("Fire1")) {
			animator.SetTrigger("Shoot");
		}

		// jumping
		if (Input.GetButtonDown ("Jump")) {
			animator.SetTrigger("Jump");
			animator.SetBool("IsOnGround", false);
			jump = true;
		}
		if (Input.GetButtonUp ("Jump")) {
			// todo: cancel the jump. megaman physics
			// todo: separate jump up/down animations? trigger each separately?
		}
	}

	void FixedUpdate () {
		// impulse velocity change on jump, then let gravity work after
		if (jump) {
			jump = false;
			velocity.y = jumpSpeed;
		} else {
			velocity.y = rigidbody2D.velocity.y;
		}
		rigidbody2D.velocity = velocity;
	}

	void OnCollisionEnter2D (Collision2D collision) {
		Debug.Log (collision.gameObject.name);
		if (collision.gameObject.name.Equals ("Ground")) {
			animator.SetBool("IsOnGround", true);
		}
	}
}
