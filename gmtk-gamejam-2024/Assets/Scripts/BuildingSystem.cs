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
        GameManager.SetBuildManager(this);

        _currentObjects.AddRange(GameObject.FindObjectsOfType<PlaceObject>());
    }

    private void Update()
    {
        if (GameManager.GetGameState() == GameState.GAME_OVER) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var nearObjects = GetObjectsInRange(worldMousePos);

            if (nearObjects.Count < 1) return;

            nearObjects = GetThreeClosest(nearObjects, worldMousePos);
            PlaceObject spawnedObject = SpawnObjectAtPos(worldMousePos);

            foreach(PlaceObject obj in nearObjects)
            {
                obj.CreateNewSpring(spawnedObject);
            }
        }

        GameManager.CheckForMaxHeight();
    }

    public List<PlaceObject> GetAllObjects() => _currentObjects;

    public PlaceObject SpawnObjectAtPos(Vector2 position)
    {
        PlaceObject newObject = Instantiate(_buildPrefab, position, Quaternion.identity);

        _currentObjects.Add(newObject);
        return newObject;
    }

    public List<PlaceObject> GetThreeClosest(List<PlaceObject> allNearest, Vector2 placePos)
    {
        if (allNearest.Count <= 3) return allNearest;

        List<PlaceObject> threeClosest = new(3);

        threeClosest.AddRange(allNearest.GetRange(0, 3));

        foreach(PlaceObject obj in allNearest)
        {
            Vector3 pos = obj.transform.position;
            float dist = Vector3.Distance(obj.transform.position, placePos);

            for(int i = 0; i < 3; i++)
            {
                if (dist < Vector3.Distance(threeClosest[i].transform.position, placePos))
                {
                    threeClosest[i] = obj;
                    break;
                }
            }
        }

        return threeClosest;
    }

    public List<PlaceObject> GetObjectsInRange(Vector2 pos)
    {
        List<PlaceObject> nearObjects = new();

        foreach(PlaceObject obj in _currentObjects)
        {
            float dist = Vector3.Distance(obj.transform.position, pos);

            ;

            if (Physics2D.Linecast(pos, obj.transform.position, ~mask))
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
