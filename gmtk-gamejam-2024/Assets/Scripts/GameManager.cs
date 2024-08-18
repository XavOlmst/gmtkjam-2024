using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public enum GameState
{
    RUNNING,
    GAME_OVER,
}

public static class GameManager
{
    [SerializeField] private static BuildingSystem _buildManager;
    private static float _bestHeight = -10000f;
    private static GameState _currentState;

    public static void SetBuildManager(BuildingSystem buildManager) => _buildManager = buildManager;
    public static void SetGameState(GameState state) => _currentState = state;
    public static float GetBestHeight() => _bestHeight;
    public static GameState GetGameState() => _currentState;

    public static void CheckForMaxHeight()
    {
        foreach(PlaceObject obj in _buildManager.GetAllObjects())
        {
            float yHeight = obj.transform.position.y;
            if (yHeight > _bestHeight)
            {
                _bestHeight = yHeight;
                Debug.Log($"Best Height is {_bestHeight}");

                //TODO: output highscore to server replacing with macAddress

                Debug.Log($"MAC Address: {GetMacAddress()}");
            }
        }
    }

    private static string GetMacAddress()
    {
        var macAdress = "";
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        var i = 0;
        foreach (NetworkInterface adapter in nics)
        {
            var address = adapter.GetPhysicalAddress();
            if (address.ToString() != "")
            {
                macAdress = address.ToString();
                return macAdress;
            }
        }

        return "error lectura mac address";
    }
}
