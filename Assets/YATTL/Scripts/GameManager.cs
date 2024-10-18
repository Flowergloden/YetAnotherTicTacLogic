using System.Collections.Generic;
using MinimaxCS;
using MonteCarloTreeSearch;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool bPlayerMove;
    public bool bCircle;
    public bool bMCTS;

    private void Awake()
    {
        Instance = this;

        bPlayerMove = Constants.Game.bPlayerMoveFirst;
        bCircle = Constants.Game.bCircleFirst;
        bMCTS = Constants.Game.bMCTS;

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
                // TODO: config it
                var mcts = new MonteCarloTree<List<List<MapData>>, Game>(1.5f, Game.Instance, 3, 10, 1000);
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
                var minimax =
                    new MinimaxTree<Game, MinimaxData>(Game.Instance,
                        new MinimaxData(LogicLayer.Instance.MapData, !bCircle), 10, true);
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