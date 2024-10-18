using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MinimaxCS;
using MonteCarloTreeSearch;
using UnityEngine;

public enum Difficulty
{
    Begin,

    A,
    B,
    C,

    End,
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

    #region flags

    private bool bStartGame = false;
    private bool bWin = false;
    private bool bThinking = false;

    #endregion

    private Task _thinkingTask;
    private CancellationTokenSource cts;

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, 200, 200));
        GUILayout.BeginVertical();
        GUILayout.Label("Difficulty");
        difficulty = (Difficulty)GUILayout.SelectionGrid((int)difficulty, Enum.GetNames(typeof(Difficulty)), 3);
        if (GUILayout.Button("开始游戏"))
        {
            bStartGame = true;
        }

        if (GUILayout.Button("退出"))
        {
            Application.Quit();
        }

        if (bThinking)
        {
            GUILayout.Label("思考中...");
        }

        if (bWin)
        {
            GUILayout.Label("游戏结束");
            if (GUILayout.Button("重新开始"))
            {
                // cts.Cancel();
                // bThinking = false;
                bWin = false;
                bStartGame = true;
            }
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void Awake()
    {
        Instance = this;
        bPlayerMove = Constants.Game.bPlayerMoveFirst;
        bCircle = Constants.Game.bCircleFirst;
        Cfg = Constants.AI.Cfg;

        LogicLayerUpdater.Instance.Initialize();
        gameObject.AddComponent<InterfaceLayer>();
        gameObject.AddComponent<WinnerDetector>();
        gameObject.AddComponent<PlacementButtonDrawer>();

        WinnerDetector.Instance.OnWin += (_, _) => { bWin = true; };

        cts = new CancellationTokenSource();
    }

    private void Update()
    {
        if (bWin)
        {
            return;
        }

        if (bStartGame)
        {
            bMCTS = CurrentCfg.bMCTS;
            bPlayerMove = Constants.Game.bPlayerMoveFirst;
            bCircle = Constants.Game.bCircleFirst;
            Cfg = Constants.AI.Cfg;

            LogicLayerUpdater.Instance.Initialize();
            Destroy(gameObject.GetComponent<InterfaceLayer>());
            gameObject.AddComponent<InterfaceLayer>();
            Destroy(gameObject.GetComponent<PlacementButtonDrawer>());
            gameObject.AddComponent<PlacementButtonDrawer>();
            bStartGame = false;
        }

        if (!bPlayerMove && !bThinking)
        {
            #region TaskDef

            var task = new Task(() =>
                {
                    bThinking = true;
                    if (bMCTS)
                    {
                        var cfg = CurrentCfg as Constants.MCTSData ??
                                  throw new ArgumentException("Chosen cfg file is not MCTS");
                        var mcts = new MonteCarloTree<List<List<MapData>>, Game>(cfg.c, Game.Instance, cfg.maxDepth,
                            cfg.gamesPerSimulation, cfg.maxIteration);
                        var res = mcts.Run();

                        // cts.Token.ThrowIfCancellationRequested();
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
                    }
                    else
                    {
                        var cfg = CurrentCfg as Constants.MinimaxData ??
                                  throw new ArgumentException("Chosen cfg file is not Minimax");

                        var minimax =
                            new MinimaxTree<Game, MinimaxData>(Game.Instance,
                                new MinimaxData(LogicLayer.Instance.MapData, !bCircle), cfg.maxDepth, true);
                        var res = minimax.Run();

                        // cts.Token.ThrowIfCancellationRequested();
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
                    }

                    bPlayerMove = true;
                    bCircle = !bCircle;
                    bThinking = false;
                }
            );

            #endregion

            task.Start();
        }
    }
}