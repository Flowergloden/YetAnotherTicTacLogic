﻿using System.Collections.Generic;
using UnityEngine;

public class MapConfigScriptableObject : ScriptableObject
{
    private static MapConfigScriptableObject _instance;

    public static MapConfigScriptableObject Instance =>
        _instance ??= Resources.Load<MapConfigScriptableObject>("Config/棋盘数据");

    public int mapSize = 3;
    public float elementSize = 1f;
    public float splitSize = 0.1f;

    public List<GameObject> elements = new();
}