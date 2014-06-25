using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoopingScript : MonoBehaviour {
	private List<Transform> backgroundPart;

	void Start()
	{
		backgroundPart = new List<Transform>();
		
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);

			if (child.renderer != null)
			{
				backgroundPart.Add(child);
			}
		}

		backgroundPart = backgroundPart.OrderBy(
			t => t.position.x
			).ToList();
	}
	
	void Update()
	{
		Transform firstChild = backgroundPart.FirstOrDefault();
		
		if (firstChild != null) {
			if (firstChild.position.x < Camera.main.transform.position.x - 2.0f) {
				if (firstChild.renderer.IsVisibleFrom(Camera.main) == false) {
					Transform lastChild = backgroundPart.LastOrDefault();
					Vector3 lastPosition = lastChild.transform.position;
					Vector3 lastSize = (lastChild.renderer.bounds.max - lastChild.renderer.bounds.min);

					firstChild.position = new Vector3(lastPosition.x + lastSize.x, firstChild.position.y, firstChild.position.z);

					backgroundPart.Remove(firstChild);
					backgroundPart.Add(firstChild);
				}
			} else if (firstChild.position.x >= Camera.main.transform.position.x - 2.0f) {
				Transform lastChild = backgroundPart.LastOrDefault();
				Vector3 firstPosition = firstChild.transform.position;
				Vector3 lastSize = (lastChild.renderer.bounds.max - lastChild.renderer.bounds.min);

				lastChild.position = new Vector3(firstPosition.x - lastSize.x, lastChild.position.y, lastChild.position.z);

				backgroundPart = backgroundPart.OrderBy(
					t => t.position.x
					).ToList();
			}
		}
	}
}
