using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    [SerializeField] private List<Sprite> _objectSprites = new(); 

    [SerializeField] private float _springDistance = 1.5f;
    [SerializeField] private float _dampingRatio = 0.3f;
    [SerializeField] private float _frequency = 1;

    private List<SpringJoint2D> _springs = new();
    private Rigidbody2D _rb;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = _objectSprites[Random.Range(0, _objectSprites.Count)];
        _springs.AddRange(GetComponents<SpringJoint2D>());
        _rb = GetComponent<Rigidbody2D>();
    }

    public Rigidbody2D GetRigidbody2D() => _rb;

    public void CreateNewSpring(Rigidbody2D connectedRigidbody)
    {
        SpringJoint2D newSpring = gameObject.AddComponent<SpringJoint2D>();
        newSpring.autoConfigureDistance = false;
        newSpring.distance = _springDistance;
        newSpring.dampingRatio = _dampingRatio;
        newSpring.frequency = _frequency;

        newSpring.connectedBody = connectedRigidbody;

        _springs.Add(newSpring);
    }
}
