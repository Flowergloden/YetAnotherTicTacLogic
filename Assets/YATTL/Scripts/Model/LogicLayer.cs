using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapElementType
{
    None,
    Circle,
    Cross,
}

public class LogicLayer
{
    private static LogicLayer _instance;
    public static LogicLayer Instance => _instance ??= new LogicLayer();

    public List<List<MapData>> MapData = new();

    public Queue<Vector2> UpdateQueue = new();
}

public class MapData
{
    public MapElementType Type = MapElementType.None;
}