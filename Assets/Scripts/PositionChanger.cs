using UnityEngine;
using System.Collections;

public class PositionChanger : MonoBehaviour
{

	public Transform target;

	private Vector3 velocity;

	private bool move;

	void Update()
	{
		if (move) {
			transform.position = Vector3.SmoothDamp(transform.position, target.transform.position, ref velocity, 0.5f);

			if (Vector3.Distance(transform.position, target.transform.position) < Vector3.kEpsilon) {
				move = false;
				transform.position = target.transform.position;
			}
		}
	}

	public void InitiatePositionChange(float completeness)
	{
		if (completeness == 1) {
			move = true;
		}
	}

}
