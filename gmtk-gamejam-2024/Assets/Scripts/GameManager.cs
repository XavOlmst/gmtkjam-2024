using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    [SerializeField] private static BuildingSystem _buildManager;
    private static float _bestHeight = -10000f;

    public static void SetBuildManager(BuildingSystem buildManager) => _buildManager = buildManager;

    public static void CheckMaxHeight()
    {
        foreach(PlaceObject obj in _buildManager.GetAllObjects())
        {
            float yHeight = obj.transform.position.y;
            if (yHeight > _bestHeight)
            {
                _bestHeight = yHeight;
                Debug.Log($"Best Height is {_bestHeight}");
            }
        }
    }
}
