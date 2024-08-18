using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeightText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    // Update is called once per frame
    void Update()
    {
        float trunc = Mathf.Floor(GameManager.GetBestHeight() * 100.0f) / 100.0f;
        _text.text = "Best Height: " + trunc.ToString() + "m";
    }
}
