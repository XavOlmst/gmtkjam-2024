using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlaceObject : MonoBehaviour
{
    [SerializeField] private List<Sprite> _objectSprites = new();
    [SerializeField] private SpringVisuals _springVisPrefab;
    [SerializeField] private List<PlaceObject> _initialConnections = new();

    [SerializeField] private float _springDistance = 1.5f;
    [SerializeField] private float _dampingRatio = 0.3f;
    [SerializeField] private float _frequency = 1;

    private List<SpringVisuals> _springs = new();
    private Rigidbody2D _rb;
    private Collider2D _col;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = _objectSprites[Random.Range(0, _objectSprites.Count)];
        _springs.AddRange(GetComponents<SpringJoint2D>());
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        foreach (PlaceObject place in _initialConnections)
        {
            CreateNewSpring(place);
        }
    }

    public Rigidbody2D GetRigidbody2D() => _rb;
    public Collider2D GetCollider2D() => _col;

    public void CreateNewSpring(PlaceObject otherObject)
    {
        SpringVisuals vis = Instantiate(_springVisPrefab, transform);

        vis.SetConnections(gameObject, otherObject.gameObject);
/*        vis.IgnoreCollisionWithCollider(_col);
        vis.IgnoreCollisionWithCollider(otherObject.GetCollider2D());
        AddSpring(vis);
        otherObject.AddSpring(vis);

        DisableSpringCollisions();
        otherObject.DisableSpringCollisions();*/

        SpringJoint2D newSpring = gameObject.AddComponent<SpringJoint2D>();
        newSpring.autoConfigureDistance = false;
        newSpring.distance = _springDistance;
        newSpring.dampingRatio = _dampingRatio;
        newSpring.frequency = _frequency;

        newSpring.connectedBody = otherObject.GetRigidbody2D();

    }

    private void AddSpring(SpringVisuals vis)
    {
        _springs.Add(vis);
    }

    private void DisableSpringCollisions()
    {
        for (int i = 0; i < _springs.Count - 1; i++)
        {
            _springs[i].IgnoreCollisionWithCollider(_springs[i + 1].GetCollider2D());
            _springs[i + 1].IgnoreCollisionWithCollider(_springs[i].GetCollider2D());
        }
    }
}
