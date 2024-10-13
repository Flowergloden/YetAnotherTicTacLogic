using System.Collections.Generic;

public class LogicLayerUpdater
{
    private static LogicLayerUpdater _instance;
    public static LogicLayerUpdater Instance => _instance ??= new LogicLayerUpdater();

    private MapConfigScriptableObject MapConfig => MapConfigScriptableObject.Instance;
    private LogicLayer LogicLayer => LogicLayer.Instance;

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
}