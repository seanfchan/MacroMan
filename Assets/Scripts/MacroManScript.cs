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
		float horizontalAxis = Input.GetAxis ("Horizontal");
		if (horizontalAxis > 0.0f) {
			animator.SetTrigger ("StartRunningRight");
			velocity.x = horizontalSpeed;
		} else if (horizontalAxis < 0.0f) {
			animator.SetTrigger ("StartRunningLeft");
			velocity.x = -horizontalSpeed;
		} else {
			animator.SetTrigger("StopRunning");
			velocity.x = 0.0f;
		}

		if (Input.GetAxis ("Vertical") > 0.0f) {
			// climb up?
		} else if (Input.GetAxis ("Vertical") < 0.0f) {
			// climb down?
		}

		if (Input.GetButtonDown("Fire1")) {
			animator.SetTrigger("Shoot");
		}

		if (Input.GetButtonDown ("Jump")) {
			animator.SetTrigger("Jump");
			jump = true;
		}
		if (Input.GetButtonUp ("Jump")) {
			// todo: cancel the jump. megaman physics
		}
	}

	void FixedUpdate () {
		if (jump) {
			jump = false;
			velocity.y = jumpSpeed;
		} else {
			velocity.y = rigidbody2D.velocity.y;
		}
		rigidbody2D.velocity = velocity;
	}
}
