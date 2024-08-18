using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class FinalScoreDisplay : MonoBehaviour
{
    [SerializeField] private float _countTime = 3f;
    [SerializeField] private TMP_Text _display;

    private void Awake()
    {
        StartCoroutine(Co_DisplayOverCountTime());
    }

    IEnumerator Co_DisplayOverCountTime()
    {
        float totalTime = 0f;

        while (totalTime < _countTime)
        {
            float percent = totalTime / _countTime;

            if (percent > 1) percent = 1;

            _display.text = $"Best Height: {GameManager.GetBestHeight() * percent}";

            yield return new WaitForEndOfFrame();
            totalTime += Time.deltaTime;
        }

        _display.text = $"Best Height: {GameManager.GetBestHeight()}";
    }

}
