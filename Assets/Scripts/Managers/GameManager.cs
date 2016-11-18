using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private CameraFollow _camFollow;

    public int Mince
    {
        get;
        private set;
    }

    private void Awake ( )
    {
        if ( Instance == null )
            Instance = this;
        else if ( Instance != this )
            Destroy ( gameObject );
    }

    public void AddMinca ( )
    {
        Mince++;
    }

    public void StartGame ( )
    {
        //  Visual change
        _camFollow.Blur ( E_FocusMode.Game );
    }

    public void GameOver ( )
    {
        //  Visual change
        _camFollow.Blur ( E_FocusMode.Menu );
    }
}
