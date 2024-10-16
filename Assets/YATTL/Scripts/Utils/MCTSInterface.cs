using System.Collections.Generic;
using System.Linq;
using MonteCarloTreeSearch;
using UnityEngine;
using Random = System.Random;

public class Game : IGame<List<List<MapData>>>
{
    public bool StartAGame(List<List<MapData>> data)
    {
        var bStartCross = !GameManager.Instance.bCross; // this latch has converted by GetPossibleMoves
        var copiedData = data.Select(x => x.Select(y => new MapData { Type = y.Type }).ToList()).ToList();

        var bCross = bStartCross;
        var random = new Random();
        do
        {
            var available = new List<Vector2>();
            for (var x = 0; x < copiedData.Count; x++)
            {
                for (var y = 0; y < copiedData[x].Count; y++)
                {
                    if (copiedData[x][y].Type == MapElementType.None)
                    {
                        available.Add(new Vector2(x, y));
                    }
                }
            }

            var index = random.Next(0, available.Count - 1);
            var tar = available[index];

            copiedData[(int)tar.x][(int)tar.y].Type = bCross
                ? MapElementType.Cross
                : MapElementType.Circle;
            bCross = !bCross;
        } while (IsEnd(copiedData));

        return bStartCross != bCross; // this latch is converted one more time at the end of loop
    }

    public List<List<MapData>> Data => LogicLayer.Instance.MapData;

    public List<List<MapData>>[] GetPossibleMoves(List<List<MapData>> data)
    {
        var available = new List<Vector2>();
        for (var x = 0; x < data.Count; x++)
        {
            for (var y = 0; y < data[x].Count; y++)
            {
                if (data[x][y].Type == MapElementType.None)
                {
                    available.Add(new Vector2(x, y));
                }
            }
        }

        return available.Select(pos =>
        {
            var newData = data.Select(x => x.Select(y => new MapData { Type = y.Type }).ToList()).ToList();
            newData[(int)pos.x][(int)pos.y].Type = GameManager.Instance.bCross
                ? MapElementType.Cross
                : MapElementType.Circle;
            return newData;
        }).ToArray();
    }

    public bool IsEnd(List<List<MapData>> data)
    {
        return WinnerDetector.Detect(data);
    }
}