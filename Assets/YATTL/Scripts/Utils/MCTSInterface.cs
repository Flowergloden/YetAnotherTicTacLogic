using System.Collections.Generic;
using System.Linq;
using MonteCarloTreeSearch;
using MinimaxCS;
using UnityEngine;
using Random = System.Random;

public class MinimaxData
{
    public List<List<MapData>> data;
    public bool bCircle;

    public MinimaxData(List<List<MapData>> data, bool bCircle)
    {
        this.data = data;
        this.bCircle = bCircle;
    }
}

public class Game : MonteCarloTreeSearch.IGame<List<List<MapData>>>, MinimaxCS.IGame<MinimaxData>
{
    private static Game _instance;
    public static Game Instance => _instance ??= new Game();

    public int StartAGame(List<List<MapData>> data)
    {
        var bStartCross = !GameManager.Instance.bCircle; // this latch has converted by GetPossibleMoves
        var copiedData = data.Select(x => x.Select(y => new MapData { Type = y.Type }).ToList()).ToList();

        var bCross = bStartCross;
        var random = new Random();

        // TODO: weighting it in MCTS library
        var bTie = false;
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

            if (!available.Any())
            {
                bTie = true;
                break;
            }

            var index = random.Next(0, available.Count - 1);
            var tar = available[index];

            copiedData[(int)tar.x][(int)tar.y].Type = bCross
                ? MapElementType.Cross
                : MapElementType.Circle;
            bCross = !bCross;
        } while (!IsEnd(copiedData));

        if (bTie)
        {
            return 0;
        }

        return bStartCross != bCross ? 1 : -1; // this latch is converted one more time at the end of loop
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
            newData[(int)pos.x][(int)pos.y].Type = GameManager.Instance.bCircle
                ? MapElementType.Circle
                : MapElementType.Cross;
            return newData;
        }).ToArray();
    }

    public bool IsEnd(List<List<MapData>> data)
    {
        return WinnerDetector.Detect(data, Constants.Map.MapSize) || GetPossibleMoves(data).Length == 0;
    }

    public float Evaluate(MinimaxData data)
    {
        var bWin = data.bCircle == GameManager.Instance.bCircle;
        if (WinnerDetector.Detect(data.data, Constants.Map.MapSize))
        {
            if (bWin)
            {
                return Constants.Minimax.Win;
            }
            else
            {
                return -Constants.Minimax.Win;
            }
        }

        return Constants.Map.MapSize switch
        {
            > 1 when WinnerDetector.Detect(data.data, Constants.Map.MapSize - 1) && bWin => Constants.Minimax
                .OneStep2Win,
            > 1 when WinnerDetector.Detect(data.data, Constants.Map.MapSize - 1) && !bWin => -Constants.Minimax
                .OneStep2Win,
            > 2 when WinnerDetector.Detect(data.data, Constants.Map.MapSize - 2) && bWin => Constants.Minimax
                .TwoSteps2Win,
            > 2 when WinnerDetector.Detect(data.data, Constants.Map.MapSize - 2) && !bWin => -Constants.Minimax
                .TwoSteps2Win,
            > 3 when WinnerDetector.Detect(data.data, Constants.Map.MapSize - 3) && bWin => Constants.Minimax
                .TreeSteps2Win,
            > 3 when WinnerDetector.Detect(data.data, Constants.Map.MapSize - 3) && bWin => -Constants.Minimax
                .TreeSteps2Win,
            _ => 0
        };
    }

    public MinimaxData[] GetPossibleMoves(MinimaxData data)
    {
        var available = new List<Vector2>();
        for (var x = 0; x < data.data.Count; x++)
        {
            for (var y = 0; y < data.data[x].Count; y++)
            {
                if (data.data[x][y].Type == MapElementType.None)
                {
                    available.Add(new Vector2(x, y));
                }
            }
        }

        var bCircle = !data.bCircle;
        return available.Select(pos =>
        {
            var newData = data.data.Select(x => x.Select(y => new MapData { Type = y.Type }).ToList()).ToList();
            newData[(int)pos.x][(int)pos.y].Type = bCircle
                ? MapElementType.Circle
                : MapElementType.Cross;
            return new MinimaxData(newData, bCircle);
        }).ToArray();
    }

    public bool IsEnd(MinimaxData data)
    {
        return IsEnd(data.data);
    }
}