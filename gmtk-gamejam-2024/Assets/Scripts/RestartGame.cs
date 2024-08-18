using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void ReloadScene()
    {
        GameManager.SetGameState(GameState.RUNNING);
        GameManager.ResetBestHeight();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
