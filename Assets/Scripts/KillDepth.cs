using UnityEngine;
using System.Collections;

public class KillDepth : MonoBehaviour
{
	public Transform triggerObject;

	public float depth;

	void Update()
	{
		if (triggerObject.position.y < depth) {
			GameObject.FindObjectOfType<GameManager>().GameOver();
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(Vector3.zero, new Vector3(0, depth, 0));
	}

}
