using System;
using System.Collections.Generic;
using MinimaxCS;
using MonteCarloTreeSearch;
using UnityEngine;
using UnityEngine.Serialization;

public enum Difficulty
{
    A,
    B,
    C,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool bPlayerMove;
    public bool bCircle;
    public bool bMCTS;

    public Dictionary<Difficulty, Constants.SearchData> Cfg;
    public Difficulty difficulty = Difficulty.A;

    public Constants.SearchData CurrentCfg => Cfg[difficulty];

    private void Awake()
    {
        Instance = this;

        bPlayerMove = Constants.Game.bPlayerMoveFirst;
        bCircle = Constants.Game.bCircleFirst;
        Cfg = Constants.AI.Cfg;
        bMCTS = CurrentCfg.bMCTS;

        LogicLayerUpdater.Instance.Initialize();
        gameObject.AddComponent<InterfaceLayer>();
        gameObject.AddComponent<WinnerDetector>();
        gameObject.AddComponent<PlacementButtonDrawer>();
    }

    private void Update()
    {
        if (!bPlayerMove)
        {
            if (bMCTS)
            {
                var cfg = CurrentCfg as Constants.MCTSData ??
                          throw new ArgumentException("Chosen cfg file is not MCTS");
                var mcts = new MonteCarloTree<List<List<MapData>>, Game>(cfg.c, Game.Instance, cfg.maxDepth,
                    cfg.gamesPerSimulation, cfg.maxIteration);
                // TODO: make it async
                var res = mcts.Run();

                for (int x = 0; x < res.Count; x++)
                {
                    for (int y = 0; y < res[0].Count; y++)
                    {
                        if (res[x][y] != LogicLayer.Instance.MapData[x][y])
                        {
                            LogicLayerUpdater.Instance.Update(new Vector2(x, y), res[x][y]);
                        }
                    }
                }

                bPlayerMove = true;
                bCircle = !bCircle;
            }
            else
            {
                var cfg = CurrentCfg as Constants.MinimaxData ??
                          throw new ArgumentException("Chosen cfg file is not Minimax");

                var minimax =
                    new MinimaxTree<Game, MinimaxData>(Game.Instance,
                        new MinimaxData(LogicLayer.Instance.MapData, !bCircle), cfg.maxDepth, true);
                var res = minimax.Run();
                for (int x = 0; x < res.data.Count; x++)
                {
                    for (int y = 0; y < res.data[0].Count; y++)
                    {
                        if (res.data[x][y] != LogicLayer.Instance.MapData[x][y])
                        {
                            LogicLayerUpdater.Instance.Update(new Vector2(x, y), res.data[x][y]);
                        }
                    }
                }

                bPlayerMove = true;
                bCircle = !bCircle;
            }
        }
    }
}