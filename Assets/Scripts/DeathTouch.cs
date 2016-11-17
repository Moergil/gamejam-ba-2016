using UnityEngine;
using System.Collections;
using UnityEditor;

public class DeathTouch : ObstacleBase
{
	public float activeVerticalOffset;
	public Vector3 targetPosition;

	public bool active;

	private Vector3 velocity;

	void Update()
	{
		if (active) {
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.5f);

			if (Vector3.Distance(transform.position, targetPosition) < Vector3.kEpsilon) {
				active = false;
			}
		}
	}

	public void Activate()
	{
		active = true;
		targetPosition = transform.position;
		targetPosition.y += activeVerticalOffset;
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Player")
			Debug.Log("TEST");
		PlayerControl playerControl = collider.gameObject.GetComponent<PlayerControl>();
		if (playerControl == null) {
			return;
		}

		GameObject.FindObjectOfType<LevelController>().Restart();
	}
}
