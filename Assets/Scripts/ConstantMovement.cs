using UnityEngine;
using System.Collections;

public class ConstantMovement : MonoBehaviour
{

	public Transform target;

	public Vector3 addition;

	void Update()
	{
		target.position += addition * Time.deltaTime;
	}
}
