using UnityEngine;
using System.Collections;

public enum E_PlayerState { Running, Idle, Jumping, Dead }

public class Player_Alex : MonoBehaviour
{
    private Animator _animator;
    private int _idleHash, _runHash, _jumpHash;

    public E_PlayerState PlayerState { get; private set; }


    #region Mono

    void Awake ( )
    {
        _animator = GetComponent<Animator> ( );

        if ( _animator != null )
        {
            _idleHash = Animator.StringToHash ( "Idle" );
            _runHash = Animator.StringToHash ( "Run" );
            _jumpHash = Animator.StringToHash ( "Jump" );
        }

        GameManager.OnGameOver += GameManager_OnGameOver;
        GameManager.OnGameStarted += GameManager_OnGameStarted;
    }

    #region Event handlers

    private void GameManager_OnGameStarted ( )
    {
        Respawn ( );
    }

    private void GameManager_OnGameOver ( )
    {
        throw new System.NotImplementedException ( );
    }

    #endregion

    void Start ( )
    {
        //  Lets run on start
        PlayerState = E_PlayerState.Running;
    }

    void Update ( )
    {
        if ( PlayerState == E_PlayerState.Running )
        {

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

    #endregion
}
