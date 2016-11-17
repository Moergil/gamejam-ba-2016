using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour
{
	bool isAutoMoving = true;
	float Speed = 5;
	Vector3 JumpVector = new Vector3(0, 20, 0);
	Vector3 Gravity = new Vector3(0, -1, 0);
	CharacterController controller;
	Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start()
	{
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (isAutoMoving) {
			if (controller.isGrounded) {
				moveDirection = new Vector3(1, 0, Input.GetAxis("Horizontal"));
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= Speed;

				if (Input.GetButton("Jump"))
					moveDirection += JumpVector;

			}

			moveDirection += Gravity;
			controller.Move(moveDirection * Time.deltaTime);
		}
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic)
			return;

		if (hit.moveDirection.y < -0.3F)
			return;

		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
		body.velocity = pushDir * 10;
	}

	public void setAutoMoving(bool value) {
		if (isAutoMoving && !value) {
			moveDirection = Vector3.zero;
		}
		isAutoMoving = value;
	}
}
