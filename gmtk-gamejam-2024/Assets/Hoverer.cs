using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverer : MonoBehaviour
{
    private Vector3 _initPos;
    [SerializeField] private float _amount = 0.3f;
    [SerializeField] private float _speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        _initPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = _initPos + Vector3.up * Mathf.Sin(Time.time * _speed) * _amount;
    }
}
