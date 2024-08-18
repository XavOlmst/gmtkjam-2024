using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringVisuals : MonoBehaviour
{
    [SerializeField] private GameObject firstObject;
    [SerializeField] private GameObject secondObject;
    // XAVIER is a nerd...lolzzzz --- :(
    [SerializeField] private float _length;
    private Collider2D _col;

    private void Awake()
    {
        _col = GetComponent<Collider2D>();
    }


    private void Start()
    {
        UpdateVisuals();
    }

    public void SetConnections(GameObject a, GameObject b)
    {
        firstObject = a;
        secondObject = b;
    }

    private void Update()
    {
        UpdateVisuals();
    }

    //Getters && setters 
    public Collider2D GetCollider2D() => _col;

    public void IgnoreCollisionWithCollider(Collider2D ignore)
    {
        Physics2D.IgnoreCollision(_col, ignore);

        Debug.Log($"Ignore collision between: {_col} & {ignore}");
    }

    private void UpdateVisuals()
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
