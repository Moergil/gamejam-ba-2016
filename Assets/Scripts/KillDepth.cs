using UnityEngine;
using System.Collections;

public class KillDepth : MonoBehaviour
{
	public LevelController levelController;
	public Transform triggerObject;

	public float depth;

	void Update()
	{
		if (triggerObject.position.y < depth) {
			levelController.Restart();
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(Vector3.zero, new Vector3(0, depth, 0));
	}

}
