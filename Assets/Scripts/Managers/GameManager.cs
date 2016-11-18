using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public static event System.Action OnGameStarted;
	public static event System.Action OnGameOver;
	public static event System.Action OnCoinCollected;

	public GameObject MincaPrefab;

	[Header("References")]

	[SerializeField]
	private CameraFollow _camFollow;

	[SerializeField]
	private Text _scoreText;

	[SerializeField]
	private Text _gameOverText;

	public Texture[] tilesTextures;

	public int Mince {
		get;
		private set;
	}

	[SerializeField]
	private Player_Alex _playerAlex;

	#region MonoBehaviour

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
	}

	private void Start()
	{
		StartGame();
	}

	#endregion

	public void AddCoin()
	{
		Mince++;
		_scoreText.text = Mince.ToString();
	}

	#region API


	public void StartGame()
	{
		//  Reset
		Mince = 0;

		//  Event
		if (OnGameStarted != null)
			OnGameStarted();

		//  Respawn
		_playerAlex.Respawn();

		//  Visual change
		_camFollow.Blur(E_FocusMode.Game);
		_gameOverText.enabled = false;
		_scoreText.text = Mince.ToString();
	}

	public void GameOver()
	{
		if (OnGameOver != null)
			OnGameOver();

		//  Visual change
		_camFollow.Blur(E_FocusMode.Menu);
		_gameOverText.enabled = true;

		//  Dead
		_playerAlex.Die();

		// TODO
		SceneManager.LoadScene(1);
	}

	#endregion
}
