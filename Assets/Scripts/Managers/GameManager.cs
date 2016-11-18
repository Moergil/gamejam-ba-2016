using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
	public GameObject MincaPrefab;


    [Header ( "References" )]

    [SerializeField]
    private CameraFollow _camFollow;

    [SerializeField]
    private Text _scoreText;

    public int Mince
    {
        get;
        private set;
    }

    #region Mono

    private void Awake ( )
    {
        if ( Instance == null )
            Instance = this;
        else if ( Instance != this )
            Destroy ( gameObject );
    }

    private void Start ( )
    {
        GameOver ( );
    }

    #endregion

    #region API

    public void AddMinca ( )
    {
        _scoreText.text = Mince++.ToString ( );
    }

    public void StartGame ( )
    {
        //  Reset
        Mince = 0;

        //  Visual change
        _camFollow.Blur ( E_FocusMode.Game );
    }

    public void GameOver ( )
    {
        //  Visual change
        _camFollow.Blur ( E_FocusMode.Menu );
    }

    #endregion
}
