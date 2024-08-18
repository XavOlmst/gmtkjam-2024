using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxer : MonoBehaviour
{
    private float _startingX, _startingY;
    [SerializeField] private float _amount;

    // Start is called before the first frame update
    void Start()
    {
        _startingX = transform.position.x;
        _startingY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = Camera.main.transform.position;
        float distX = camPos.x * _amount;
        float distY = camPos.y * _amount;

        transform.position = new(
            _startingX + distX, _startingY + distY, transform.position.z
            );
    }
}
