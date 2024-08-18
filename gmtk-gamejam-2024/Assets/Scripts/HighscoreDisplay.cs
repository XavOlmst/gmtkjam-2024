using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HighscoreDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _textPrefab;
    [SerializeField] private Color _highlightColor;
    private List<float> _allHighscores = new();

    private void Awake()
    {
        _allHighscores.Clear();
        LoadHighscores();

        SortHighscores();
        UpdateHighscoreVisuals();
    }

    public void LoadHighscores()
    {
        HighscoreData highscores = SaveLoadSystem.LoadHighscore();
        if (highscores == null) return;

        foreach(float highscore in highscores._highscores)
        {
            if (highscore <= 0) continue;

            _allHighscores.Add(highscore);
        }
    }

    private void SortHighscores()
    {
        _allHighscores.Sort();
        _allHighscores.Reverse();
    }

    public void UpdateHighscoreVisuals()
    {
        bool inTopTen = false;

        int maxScores = _allHighscores.Count > 10 ? 10 : _allHighscores.Count;

        for (int i = 0; i < maxScores; i++)
        {
            var text = Instantiate(_textPrefab, _panel.transform);

            float highscore = _allHighscores[i];
            text.text = $"{i + 1}: {highscore.ToString("F2")}";

            if (Mathf.Approximately(highscore, GameManager.GetBestHeight()))
            {
                inTopTen = true;
                text.color = _highlightColor;
            }
        }

        if(!inTopTen)
        {
            for (int i = 0; i < _allHighscores.Count; i++)
            {
                float highscore = _allHighscores[i];
                if (Mathf.Approximately(highscore, GameManager.GetBestHeight()))
                {
                    TMP_Text text = Instantiate(_textPrefab, _panel.transform);
                    text.text = $"{i + 1}: {highscore.ToString("F2")}";
                    text.color = _highlightColor;
                }
            }
        }
    }
}
