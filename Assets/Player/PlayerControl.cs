﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour {
	public float Speed = 5;
	public float JumpSpeed = 13;
	public float Gravity = 40;

	CharacterController controller;
	private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (controller.isGrounded) {
			moveDirection = new Vector3(1, 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= Speed;
			if (Input.GetButton("Jump"))
				moveDirection.y = JumpSpeed;

		}

		moveDirection.y -= Gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}
}