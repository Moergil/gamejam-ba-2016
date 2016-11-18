using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public GameObject MincaPrefab;

    public static event System.Action OnGameStarted;
    public static event System.Action OnGameOver;

	[Header("References")]

	[SerializeField]
	private CameraFollow _camFollow;

	[SerializeField]
	private Text _scoreText;

	public int Mince {
		get;
		private set;
	}

    [SerializeField]
    private PlayerControl _player;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
	}

	private void Start()
	{
	}

	#endregion

	#region API

	public void AddMinca()
	{
		if (_scoreText != null) {
			_scoreText.text = Mince++.ToString();
		}
	}

	public void StartGame()
	{
		//  Reset
		Mince = 0;

		//  Event
        if ( OnGameStarted != null ) OnGameStarted ( );

        //  Visual change
        _camFollow.Blur ( E_FocusMode.Game );
	}

    public void GameOver ( )
    {
        if ( OnGameOver != null ) OnGameOver ( );

        //  Visual change
        _camFollow.Blur ( E_FocusMode.Menu );
    }

	#endregion
}
