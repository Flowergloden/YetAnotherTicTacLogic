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

    public readonly List<List<MapData>> MapData = new();

    public readonly Queue<Vector2> UpdateQueue = new();
}

public record MapData
{
    public MapElementType Type = MapElementType.None;
}