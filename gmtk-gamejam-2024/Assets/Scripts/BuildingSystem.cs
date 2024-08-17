using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] private PlaceObject _buildPrefab;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float _spawnRange = 2;
    [SerializeField] private float _minRange = 0.5f; 

    private List<PlaceObject> _currentObjects = new();

    private void Awake()
    {
        _currentObjects.AddRange(GameObject.FindObjectsOfType<PlaceObject>());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var nearObjects = GetObjectsInRange(worldMousePos);

            if (nearObjects.Count < 1) return;

            PlaceObject spawnedObject = SpawnObjectAtPos(worldMousePos);

            foreach(PlaceObject obj in nearObjects)
            {
                obj.CreateNewSpring(spawnedObject.GetRigidbody2D());
                //spawnedObject.CreateNewSpring(obj.GetRigidbody2D());
            }
        }
    }

    public PlaceObject SpawnObjectAtPos(Vector2 position)
    {
        PlaceObject newObject = Instantiate(_buildPrefab, position, Quaternion.identity);

        _currentObjects.Add(newObject);
        return newObject;
    }


    public List<PlaceObject> GetObjectsInRange(Vector2 pos)
    {
        List<PlaceObject> nearObjects = new();

        foreach(PlaceObject obj in _currentObjects)
        {
            float dist = Vector3.Distance(obj.transform.position, pos);

            if (Physics.Raycast(pos, (Vector2)obj.transform.position - pos, dist, mask))
            {
                continue;
            }

            if (dist < _spawnRange)
            {
                if(dist < _minRange)
                {
                    nearObjects.Clear();
                    return nearObjects;
                }

                nearObjects.Add(obj);
            }
        }

        return nearObjects;
    }
}
