using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InterfaceLayer : MonoBehaviour
{
    public static InterfaceLayer Instance;

    private static LogicLayer LogicLayer => LogicLayer.Instance;
    private static MapConfigScriptableObject MapConfig => MapConfigScriptableObject.Instance;

    private static Camera MainCamera => Camera.main;
    private GameObject _map;
    public readonly Dictionary<Vector2, GameObject> Generated = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _map = new GameObject("Map");
        var delta = MapConfig.elementSize + MapConfig.splitSize;

        MainCamera.transform.position =
            new Vector3((MapConfig.mapSize / 2f - 0.5f * MapConfig.elementSize) * delta,
                (MapConfig.mapSize / 2f - 0.5f * MapConfig.elementSize) * delta, -10f);

        for (int x = 0; x < LogicLayer.MapData.Count; x++)
        {
            for (int y = 0; y < LogicLayer.MapData[0].Count; y++)
            {
                var obj = Instantiate(MapConfig.elements[(int)LogicLayer.MapData[x][y].Type],
                    new Vector3(x * delta, y * delta, 0),
                    Quaternion.identity, _map.transform);
                obj.transform.localScale = new Vector3(MapConfig.elementSize, MapConfig.elementSize, 1);
                Generated[new Vector2(x, y)] = obj;
            }
        }
    }

    private void Update()
    {
        while (LogicLayer.UpdateQueue.Any())
        {
            var pos = LogicLayer.UpdateQueue.Dequeue();
            Destroy(Generated[pos]);

            var delta = MapConfig.elementSize + MapConfig.splitSize;
            var x = (int)pos.x;
            var y = (int)pos.y;
            var obj = Instantiate(MapConfig.elements[(int)LogicLayer.MapData[x][y].Type],
                new Vector3(x * delta, y * delta, 0),
                Quaternion.identity, _map.transform);
            obj.transform.localScale = new Vector3(MapConfig.elementSize, MapConfig.elementSize, 1);
            Generated[new Vector2(x, y)] = obj;
        }
    }
}