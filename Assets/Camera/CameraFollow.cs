﻿using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
	public Transform Target;
	public Vector3 Offset = new Vector3(-2.41f, 4.13f, -4.1f);
	public Vector3 Rotation = new Vector3(30.631f, 54.883f, 3.34f);
	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start()
	{
		transform.position = normalizedTargetObjectPosition();
		transform.rotation = Quaternion.Euler (Rotation);
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
