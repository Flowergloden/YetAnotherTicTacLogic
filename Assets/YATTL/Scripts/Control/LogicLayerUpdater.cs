using System.Collections.Generic;
using UnityEngine;

public class LogicLayerUpdater
{
    private static LogicLayerUpdater _instance;
    public static LogicLayerUpdater Instance => _instance ??= new LogicLayerUpdater();

    private static MapConfigScriptableObject MapConfig => MapConfigScriptableObject.Instance;
    private static LogicLayer LogicLayer => LogicLayer.Instance;

    public void Initialize()
    {
        LogicLayer.MapData.Clear();

        for (int x = 0; x < MapConfig.mapSize; x++)
        {
            LogicLayer.MapData.Add(new List<MapData>());
            for (int y = 0; y < MapConfig.mapSize; y++)
            {
                LogicLayer.MapData[x].Add(new MapData());
            }
        }
    }

    public void Update(Vector2 pos, MapData data)
    {
        LogicLayer.MapData[(int)pos.x][(int)pos.y] = data;
        LogicLayer.UpdateQueue.Enqueue(pos);
    }
}