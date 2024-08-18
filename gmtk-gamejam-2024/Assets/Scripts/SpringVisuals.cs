using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringVisuals : MonoBehaviour
{
    [SerializeField] private GameObject firstObject;
    [SerializeField] private GameObject secondObject;
    // XAVIER is a nerd...lolzzzz
    [SerializeField] private float _length;

    public void SetConnections(GameObject a, GameObject b)
    {
        firstObject = a;
        secondObject = b;
    }

    private void Update()
    {
        transform.position = firstObject.transform.position;
        transform.localScale = new Vector3(
            Vector3.Distance(firstObject.transform.position, secondObject.transform.position) / _length,
            1, 1
        );
        var a = new Vector3(transform.position.x, transform.position.y, 0);
        var b = new Vector3(secondObject.transform.position.x, secondObject.transform.position.y, 0);
        transform.right = Vector3.Normalize(b - a);
    }
}
