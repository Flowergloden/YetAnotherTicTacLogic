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
        foreach (var line in LogicLayer.MapData)
        {
            Detect(line);
        }

        var transposed = LogicLayer.MapData.Select((x, i) => LogicLayer.MapData.Select(y => y[i]).ToList()).ToList();
        foreach (var row in transposed)
        {
            Detect(row);
        }

        var diagonal1 = LogicLayer.MapData.Select((_, i) => LogicLayer.MapData[i][i]).ToList();
        Detect(diagonal1);
        var diagonal2 = LogicLayer.MapData.Select((_, i) =>
            LogicLayer.MapData[i][LogicLayer.MapData.Count - i - 1]).ToList();
        Detect(diagonal2);
    }

    private void Detect(List<MapData> data)
    {
        if (data.GroupBy(x => x.Type).Count() == 1)
        {
            if (data[0].Type == MapElementType.None)
            {
                return;
            }

            OnWin?.Invoke(this, EventArgs.Empty);
        }
    }
}