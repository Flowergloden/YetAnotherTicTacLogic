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
        if (Detect(LogicLayer.MapData, Constants.Map.MapSize))
        {
            OnWin?.Invoke(null, EventArgs.Empty);
        }
    }

    public static bool Detect(List<List<MapData>> data, int num)
    {
        if (data.Any(line => InnerDetect(line, num)))
        {
            return true;
        }

        var transposed = data.Select((x, i) => data.Select(y => y[i]).ToList()).ToList();
        if (transposed.Any(row => InnerDetect(row, num)))
        {
            return true;
        }

        var diagonal1 = data.Select((_, i) => data[i][i]).ToList();

        var diagonal2 = data.Select((_, i) =>
            data[i][data.Count - i - 1]).ToList();

        return InnerDetect(diagonal1, num) || InnerDetect(diagonal2, num);
    }

    private static bool InnerDetect(List<MapData> data, int num)
    {
        if (data.GroupBy(mapData => mapData.Type)
            .Where(group => group.Key != MapElementType.None)
            .Any(group => group.Count() == num))
        {
            return data[0].Type != MapElementType.None;
        }

        return false;
    }
}