using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    #region Mono

    private void Awake ( )
    {
        DontDestroyOnLoad ( gameObject );
    }

    #endregion

    #region API

    public void LoadLevel ( int levelNo )
    {
        SceneManager.LoadScene ( levelNo);
    }

    #endregion
}
