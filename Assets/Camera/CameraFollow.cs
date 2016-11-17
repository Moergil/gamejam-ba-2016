using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
	public Transform Target;
	public Vector2 DistanceBounds = new Vector2(1, 10);
	public Vector3 Offset = new Vector3(-0.2f, 6.96f, -4.53f);

	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start()
	{
		transform.position = normalizedTargetObjectPosition();
	}
	
	// Update is called once per frame
	void Update()
	{
		transform.position = Vector3.SmoothDamp(transform.position, normalizedTargetObjectPosition(), ref velocity, 0.5f);
	}

	Vector3 normalizedTargetObjectPosition()
	{
		return new Vector3(Target.position.x, 0, 0) + Offset;
	}

}
