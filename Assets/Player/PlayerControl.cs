using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour
{
	bool isAutoMoving = true;
	public float Speed = 3;
	public float ActualSpeed;
	public float SpeedBonus = 0;
	Vector3 JumpVector = new Vector3(0, 4, 0);
	Vector3 Gravity = new Vector3(0, -0.4f, 0);
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
				ActualSpeed = Speed;
				if (Input.GetAxis ("Vertical") > 0) {
					ActualSpeed += Input.GetAxis ("Vertical") * (Speed / 6);
				} else {
					ActualSpeed += Input.GetAxis ("Vertical") * (Speed / 2);
				}

				moveDirection = new Vector3(1, 0, -Input.GetAxis("Horizontal"));
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= ActualSpeed + SpeedBonus;

				if (Input.GetButtonDown ("Jump")) {
					moveDirection += JumpVector;
				}
			}

			if (Input.GetButton ("Duck")) {
				transform.localScale = new Vector3 (1, 0.5f, 1);
			} else {
				transform.localScale = Vector3.one;
			}
		}
		SpeedBonus += Time.deltaTime * 0.1f + SpeedBonus * Time.deltaTime * 0.1f;
		SpeedBonus = Mathf.Clamp (SpeedBonus, 0, 10);
		moveDirection += Gravity;
		controller.Move(moveDirection * Time.deltaTime);
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.gameObject.tag == "Minca") {
			GameManager.Instance.AddMinca ();
			Destroy (hit.gameObject);
			return;
		}

		if (hit.moveDirection.y < -0.3F)
			return;
		
		SpeedBonus = SpeedBonus * 0.9f;

		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic)
			return;

		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
		body.velocity = pushDir * 10;
	}

	public void setAutoMoving(bool value)
	{
		if (isAutoMoving && !value) {
			moveDirection = Vector3.zero;
		}
		isAutoMoving = value;
	}
}
