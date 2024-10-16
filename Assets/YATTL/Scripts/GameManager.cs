using System;
using System.Collections.Generic;
using MonteCarloTreeSearch;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool bPlayerMove;
    public bool bCircle;

    private void Awake()
    {
        Instance = this;

        bPlayerMove = Constants.Game.bPlayerMoveFirst;
        bCircle = Constants.Game.bCircleFirst;

        LogicLayerUpdater.Instance.Initialize();
        gameObject.AddComponent<InterfaceLayer>();
        gameObject.AddComponent<WinnerDetector>();
        gameObject.AddComponent<PlacementButtonDrawer>();
    }

    private void Update()
    {
        if (!bPlayerMove)
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
    }
}