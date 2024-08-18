using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private EndGameCanvas _gameOverCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.GetGameState() == GameState.GAME_OVER) return;

        EndTheGame();
    }

    private void EndTheGame()
    {
        HighscoreData data = SaveLoadSystem.LoadHighscore();
        
        if(data == null)
        {
            data = new();
        }

        SortUpdatedHighscores(data);

        SaveLoadSystem.SaveHighscore(data);

        GameManager.SetGameState(GameState.GAME_OVER);
        _gameOverCanvas.gameObject.SetActive(true);
        _gameOverCanvas.StartTheSlides();
    }

    private void SortUpdatedHighscores(HighscoreData data)
    {
        List<float> highscores = new(data._highscores)
        {
            GameManager.GetBestHeight()
        };

        highscores.Sort();
        highscores.Reverse();

        data._highscores = highscores.GetRange(0, 10).ToArray();
    }
}
