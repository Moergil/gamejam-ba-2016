using UnityEngine;
using System.Collections;

public class PlayerBounds : MonoBehaviour
{
	public Transform Target;

	// Use this for initialization
	void Start()
	{
		transform.position = Target.position;
	}
	
	// Update is called once per frame
	void LateUpdate()
	{
		transform.position = new Vector3(Target.position.x, transform.position.y, transform.position.z);
	}
}
