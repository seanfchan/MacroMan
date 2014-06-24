using UnityEngine;
using System.Collections;

public class MacroManScript : MonoBehaviour {

	private Animator animator;

	public float speed = 10.0f;

	private Vector2 velocity = new Vector2 (0.0f, 0.0f);

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		float horizontalAxis = Input.GetAxis ("Horizontal");
		if (horizontalAxis > 0.0f) {
			animator.SetTrigger ("StartRunningRight");
			velocity.x = speed;
		} else if (horizontalAxis < 0.0f) {
			animator.SetTrigger ("StartRunningLeft");
			velocity.x = -speed;
		} else {
			animator.SetTrigger("StopRunning");
			velocity.x = 0.0f;
		}
		if (Input.GetAxis ("Vertical") > 0.0f) {
			// nothing?
		} else if (Input.GetAxis ("Vertical") < 0.0f) {
			// nothing?
		}
//		if (Input.GetKeyDown (KeyCode.W)) {
//			animator.SetTrigger("StartRunning");
//		}
//		if (Input.GetKeyUp (KeyCode.W)) {
//			animator.SetTrigger ("StopRunning");
//		}
	}

	void FixedUpdate () {
		rigidbody2D.velocity = velocity;
	}
}
