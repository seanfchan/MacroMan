﻿using UnityEngine;
using System.Collections;

public class MacroManScript : MonoBehaviour {
	public float horizontalSpeed = 10.0f;
	public float jumpSpeed = 10.0f;

	private Vector2 velocity = new Vector2 (0.0f, 0.0f);
	private Animator animator;

	private bool jump = false;
	private bool jumpCancel = false;

	private bool isSlideDashing = false;

	void Start () {
		animator = GetComponent<Animator> ();
	}

	void Update () {
		// movement
		float slideDashFactor = (isSlideDashing) ? 1.5f : 1.0f;
		float horizontalAxis = Input.GetAxis ("Horizontal");
		if (horizontalAxis > 0.0f) {
			animator.SetTrigger ("StartRunningRight");
			animator.SetBool ("IsRunning", true);
			velocity.x = horizontalSpeed * slideDashFactor;
		} else if (horizontalAxis < 0.0f) {
			animator.SetTrigger ("StartRunningLeft");
			animator.SetBool ("IsRunning", true);
			velocity.x = -horizontalSpeed * slideDashFactor;
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
		if (Input.GetButtonDown("Shoot")) {
			animator.SetTrigger ("Shoot");
		}

		// jumping
		if (Input.GetButtonDown ("Jump")
		    && animator.GetBool ("IsOnGround")) {
			animator.SetTrigger ("Jump");
			animator.SetBool ("IsOnGround", false);
			jump = true;
		}
		if (Input.GetButtonUp ("Jump")) {
			// todo: separate jump up/down animations? trigger each separately?
			jumpCancel = true;
		}

		// slide dashing
		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("SlideDashRight")
		    && !animator.GetCurrentAnimatorStateInfo (0).IsName ("SlideDashLeft")
		    && animator.GetBool ("IsOnGround")) {
			isSlideDashing = false;
		}
		if (Input.GetButtonDown ("SlideDash")
		    && animator.GetBool ("IsRunning")
		    && animator.GetBool ("IsOnGround")) {
			animator.SetTrigger ("SlideDash");
			isSlideDashing = true;
			// todo: increase speed until dash is done/interrupted
		}
	}

	void FixedUpdate () {
		// impulse velocity change on jump, then let gravity work after
		if (jump) {
			jump = false;
			velocity.y = jumpSpeed;
		} else if (jumpCancel) {
			jumpCancel = false;
			if (rigidbody2D.velocity.y > 0.0f) {
				velocity.y = 0.0f;
			}
		} else {
			velocity.y = rigidbody2D.velocity.y;
		}
		rigidbody2D.velocity = velocity;
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.name.Equals ("Ground")) {
			animator.SetBool ("IsOnGround", true);
		}
	}
}
