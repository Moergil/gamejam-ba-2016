using UnityEngine;
using System.Collections;

public enum E_PlayerState { Running, Idle, Jumping, Dead }

[RequireComponent ( typeof ( Rigidbody ) )]
public class Player_Alex : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rb;

    private int _idleHash, _runHash, _jumpHash;

    public E_PlayerState PlayerState { get; private set; }

            Vector3 moveDirection;

    #region Mono

    void Awake ( )
    {
        _animator = GetComponent<Animator> ( );
        _rb = GetComponent<Rigidbody> ( );

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

            moveDirection = new Vector3 ( 1, 0, -Input.GetAxis ( "Horizontal" ) );
            moveDirection = transform.TransformDirection ( moveDirection );
        }
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

    void OnCollisionEnter ( Collider col )
    {
        if ( col.tag == "Minca" )
        {
            GameManager.Instance.AddCoin ( );
            Destroy ( col.gameObject );
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
