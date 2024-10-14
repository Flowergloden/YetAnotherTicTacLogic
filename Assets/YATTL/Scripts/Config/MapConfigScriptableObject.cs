using UnityEngine;

public class MapConfigScriptableObject : ScriptableObject
{
    private static MapConfigScriptableObject _instance;

    public static MapConfigScriptableObject Instance =>
        _instance ??= Resources.Load<MapConfigScriptableObject>("Config/棋盘数据");

    public int mapSize = 3;
}