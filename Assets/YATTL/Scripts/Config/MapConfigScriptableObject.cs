using UnityEngine;

public class MapConfigScriptableObject : ScriptableObject
{
    private static MapConfigScriptableObject _instance;

    public static MapConfigScriptableObject Instance
    {
        get
        {
            if (_instance is not null)
            {
                return _instance;
            }

            var assets = FindObjectsOfType<MapConfigScriptableObject>();
            switch (assets.Length)
            {
                case > 1:
                    Debug.LogError("Duplicate MapConfig found");
                    break;
                case 0:
                    Debug.LogError("No MapConfig found");
                    break;
            }

            return assets[0];
        }
    }

    public int mapSize = 3;
}