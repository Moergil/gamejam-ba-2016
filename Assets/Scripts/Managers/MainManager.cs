using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
	#region API

	public void LoadLevel(int levelNo)
	{
		SceneManager.LoadScene(levelNo);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	#endregion
}
