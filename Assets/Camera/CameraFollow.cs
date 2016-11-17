using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour {
	public GameObject TargetObject;
	public Vector2 DistanceBounds = new Vector2 (1, 10);
	public float Scale = 0.1f;

	// Use this for initialization
	void Start () {
		transform.position = normalizedTargetObjectPosition ();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		/*Vector3 distance = normalizedTargetObjectPosition () - transform.position;
		if (distance.magnitude <= DistanceBounds.x) {
			distance = Vector3.zero;
		}
		else if (distance.magnitude <= DistanceBounds.y) {
			distance -= distance.normalized * (distance.magnitude - DistanceBounds.x);
		}
			
		transform.position += distance;*/
		Vector3 velocity = Vector3.zero;
		transform.position = Vector3.SmoothDamp (transform.position, normalizedTargetObjectPosition (), ref velocity, 0.1f);
	}

	Vector3 normalizedTargetObjectPosition() {
		return new Vector3 (TargetObject.transform.position.x, 7, -6);
	}

	public static Vector3 Lerp(Vector3 a, Vector3 b, float t) {
		return new Vector3(Mathf.Lerp(a.x, b.x, t), Mathf.Lerp(a.y, b.y, t), Mathf.Lerp(a.z, b.z, t));
	}
}
