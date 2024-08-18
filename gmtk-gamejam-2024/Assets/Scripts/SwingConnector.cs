using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingConnector : MonoBehaviour
{
    [SerializeField] private Transform _swingConnection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.right = Vector3.Normalize(_swingConnection.position - transform.position);
    }
}
