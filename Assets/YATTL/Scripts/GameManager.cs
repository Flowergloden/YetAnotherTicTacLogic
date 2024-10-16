using System;
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
        // TODO: Test only
        bPlayerMove = true;
    }
}