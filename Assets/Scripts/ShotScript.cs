using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {
	void Update () {
		if (transform.renderer.IsVisibleFrom (Camera.main) == false) {
			MoveScript moveScript = transform.GetComponent<MoveScript> ();
			if (moveScript != null) {
				Destroy (gameObject);
			}
		}
	}
}
