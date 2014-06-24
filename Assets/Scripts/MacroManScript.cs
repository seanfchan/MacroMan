using UnityEngine;
using System.Collections;

public class MacroManScript : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		float horizontalAxis = Input.GetAxis ("Horizontal");
		if (horizontalAxis > 0.0f) {
			animator.SetTrigger ("StartRunning");
		} else if (horizontalAxis < 0.0f) {
			// move left
		} else {
			animator.SetTrigger("StopRunning");
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
}
