using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasObject : MonoBehaviour
{
    [SerializeField] private Vector2 _endCanvasPos;
    [SerializeField] private float _lerpTime = 0.2f;
    private bool _isSliding = false;
    private RectTransform _rectTransform;
    private float _timeLeft;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _timeLeft = _lerpTime;
    }

    private void Update()
    {
        if (!_isSliding) return;

        float percent = _timeLeft / _lerpTime;
        if (percent > 1) percent = 1;
        if (percent < 0) percent = 0;

        _rectTransform.localPosition = Vector2.Lerp(_rectTransform.localPosition, _endCanvasPos, 1 - percent);

        _timeLeft -= Time.deltaTime;
    }

    public void StartSlide() => _isSliding = true;
}
