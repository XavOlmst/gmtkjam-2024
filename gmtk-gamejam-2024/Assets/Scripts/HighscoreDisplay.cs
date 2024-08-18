using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HighscoreDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _textPrefab;
    [SerializeField] private Color _highlightColor;
    [SerializeField] private List<float> _allHighscores = new();

    private void Awake()
    {
        SortHighscores();
        UpdateHighscoreVisuals();
    }

    public void LoadHighscores()
    {
        //TODO: load highscore data from server...
        _allHighscores = new();
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
            text.text = $"{i + 1}: {highscore}";

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
                    text.text = $"{i + 1}: {highscore}";
                    text.color = _highlightColor;
                }
            }
        }
    }
}
