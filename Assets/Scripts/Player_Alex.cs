using UnityEngine;
using System.Collections;

public enum E_PlayerState { Running, Idle, Jumping, Dead }

public class Player_Alex : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rb;
    CharacterController _controller;

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    Vector3 Gravity = new Vector3 ( 0, -0.4f, 0 );

    private Vector3 moveDirection = Vector3.zero;
    public float ActualSpeed;
    public float SpeedBonus = 0;

    private int _idleHash, _runHash, _jumpHash;

    public E_PlayerState PlayerState { get; private set; }

    [SerializeField]
    private Vector3 _jumpVector = new Vector3 ( 0f, 1f, 0f );

    [SerializeField]
    private Vector3 _gravity = new Vector3 ( 0f, 9.8f, 0f );

    #region Mono

    void Awake ( )
    {
        _animator = GetComponentInChildren<Animator> ( );
        _rb = GetComponent<Rigidbody> ( );
        _controller = GetComponent<CharacterController> ( );

        if ( _animator != null )
        {
            _idleHash = Animator.StringToHash ( "Idle" );
            _runHash = Animator.StringToHash ( "Run" );
            _jumpHash = Animator.StringToHash ( "Jump" );
        }

        if ( _rb != null )
            _rb.isKinematic = true;

        GameManager.OnGameOver += GameManager_OnGameOver;
        GameManager.OnGameStarted += GameManager_OnGameStarted;
        GameManager.OnCoinCollected += GameManager_OnCoinCollected;
    }

    void Start ( )
    {
        //  Lets run on start
        PlayerState = E_PlayerState.Running;
    }

    void Update ( )
    {
        if ( PlayerState == E_PlayerState.Running )
        {

            moveDirection = Vector3.right * speed;
            moveDirection += new Vector3 ( 1f, 0f, Input.GetAxis ( "Horizontal" ) );

            if ( Input.GetButtonDown ( "Jump" ) )
            {
                moveDirection += _jumpVector;
            }
        }

        moveDirection += Gravity;
        _controller.Move ( moveDirection * Time.deltaTime );
    }

    #endregion

    #region API

    public void Respawn ( )
    {
        PlayerState = E_PlayerState.Running;
    }

    public void Die ( )
    {
        PlayerState = E_PlayerState.Dead;
    }

    public void Jump ( )
    {
        _animator.SetTrigger ( _jumpHash );
    }

    public void Run ( )
    {
        _animator.SetTrigger ( _runHash );
    }

    public void Idle ( )
    {
        _animator.SetTrigger ( _idleHash );
    }

    #endregion

    #region Collisions

    void OnControllerColliderHit ( ControllerColliderHit hit )
    {
        if ( hit.gameObject.tag == "Minca" )
        {
            GameManager.Instance.AddCoin ( );
            Destroy ( hit.gameObject );
            return;
        }
    }

    #endregion

    #region Event handlers

    private void GameManager_OnGameStarted ( )
    {
        Respawn ( );
    }

    private void GameManager_OnGameOver ( )
    {
        Die ( );
    }

    private void GameManager_OnCoinCollected ( )
    {
        throw new System.NotImplementedException ( );
    }

    #endregion
}
