using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinnerDetector : MonoBehaviour
{
    private static LogicLayer LogicLayer => LogicLayer.Instance;
    public event EventHandler OnWin = (_, _) => { Debug.Log("Winner"); };

    private void Update()
    {
        if (Detect(LogicLayer.MapData))
        {
            OnWin?.Invoke(null, EventArgs.Empty);
        }
    }

    public static bool Detect(List<List<MapData>> data)
    {
        if (data.Any(InnerDetect))
        {
            return true;
        }

        var transposed = data.Select((x, i) => data.Select(y => y[i]).ToList()).ToList();
        if (transposed.Any(InnerDetect))
        {
            return true;
        }

        var diagonal1 = data.Select((_, i) => data[i][i]).ToList();

        var diagonal2 = data.Select((_, i) =>
            data[i][data.Count - i - 1]).ToList();

        return InnerDetect(diagonal1) || InnerDetect(diagonal2);
    }

    private static bool InnerDetect(List<MapData> data)
    {
        if (data.GroupBy(x => x.Type).Count() == 1)
        {
            return data[0].Type != MapElementType.None;
        }

        return false;
    }
}