using UnityEngine;
using System.Collections;

public enum E_PlayerState
{
	Running,
	Idle,
	Jumping,
	Dead

}

public class Player_Alex : MonoBehaviour
{
	private Animator _animator;
	private Rigidbody _rb;
	CharacterController _controller;

	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;

	private Vector3 moveDirection = Vector3.zero;
	public float ActualSpeed;
	public float SpeedBonus = 0;

    private int _idleHash, _runHash, _jumpHash, _deadHash;

	public E_PlayerState PlayerState { get; private set; }

	private float gravity = 9.8f;

	#region Mono

	void Awake()
	{
		_animator = GetComponentInChildren<Animator>();
		_rb = GetComponent<Rigidbody>();
		_controller = GetComponent<CharacterController>();

		if (_animator != null) {
			_idleHash = Animator.StringToHash("Idle");
			_runHash = Animator.StringToHash("Run");
			_jumpHash = Animator.StringToHash("Jump");
			_deadHash = Animator.StringToHash("Die");
        }

		if (_rb != null)
			_rb.isKinematic = true;

		GameManager.OnGameOver += GameManager_OnGameOver;
		GameManager.OnGameStarted += GameManager_OnGameStarted;
		GameManager.OnCoinCollected += GameManager_OnCoinCollected;
	}

	void Start()
	{
		//  Lets run on start
		PlayerState = E_PlayerState.Running;
	}

	void Update()
	{
		if (_controller.isGrounded) {
			float steeringInput = -Input.GetAxis("Horizontal");
			moveDirection = new Vector3(1, 0, steeringInput);
			moveDirection *= speed;
			Debug.DrawRay(transform.position, moveDirection, Color.red, 0, false);
			if (Input.GetButton("Jump"))
            {
                Jump ( );
				moveDirection.y = jumpSpeed;
            }
            
		}

		moveDirection.y -= gravity * Time.deltaTime;
		_controller.Move(moveDirection * Time.deltaTime);
	}

	public void ApplyForce(Vector3 force)
	{
		moveDirection += force;
	}

	#endregion

	#region API

	public void Respawn()
	{
		Run();
	}

	public void Die()
	{
        PlayerState = E_PlayerState.Dead;
        _animator.SetTrigger(_deadHash);
    }

    public void Jump()
	{
		PlayerState = E_PlayerState.Jumping;
        _animator.SetTrigger(_jumpHash);
	}

	public void Run()
	{
		PlayerState = E_PlayerState.Running;
		_animator.SetTrigger(_runHash);
	}

	public void Idle()
	{
		PlayerState = E_PlayerState.Idle;
        _animator.SetTrigger(_idleHash);
	}

	#endregion

	#region Collisions

	void OnTriggerEnter(Collider collider)
	{
		GameObject colliderObject = collider.gameObject;
		if (colliderObject.tag == "Minca") {
			GameManager.Instance.AddCoin();
			Destroy(colliderObject);
			return;
		}
	}

	#endregion

	#region Event handlers

	private void GameManager_OnGameStarted()
	{
		Respawn();
	}

	private void GameManager_OnGameOver()
	{
		Die();
	}

	private void GameManager_OnCoinCollected()
	{
		throw new System.NotImplementedException();
	}

	#endregion
}
