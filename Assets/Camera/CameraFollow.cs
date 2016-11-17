using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
	public Transform Target;
	public Vector3 Offset;

	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start()
	{
		Offset = transform.position - Target.position;
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
