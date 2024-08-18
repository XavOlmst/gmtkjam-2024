using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("LoadNextScene", 1f);
    }

    private void LoadNextScene()
    {
        HighscoreData data = SaveLoadSystem.LoadHighscore();
        
        if(data == null)
        {
            data = new();
        }

        SortUpdatedHighscores(data);

        SaveLoadSystem.SaveHighscore(data);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    private void SortUpdatedHighscores(HighscoreData data)
    {
        List<float> highscores = new(data._highscores);

        if (highscores.Count >= 10)
        {
            highscores.Sort();
            highscores.Reverse();

            for (int i = 0; i < highscores.Count; i++)
            {
                float score = highscores.Count;
                if (GameManager.GetBestHeight() > score)
                {
                    highscores[i] = GameManager.GetBestHeight();
                    break;
                }
            }
        }
        else
        {
            highscores.Add(GameManager.GetBestHeight());
        }

        data._highscores = highscores.ToArray();
    }
}
