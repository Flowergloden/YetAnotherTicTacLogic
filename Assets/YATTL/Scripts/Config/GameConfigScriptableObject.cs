using UnityEngine;

public class GameConfigScriptableObject : ScriptableObject
{
    private static GameConfigScriptableObject _instance;

    public static GameConfigScriptableObject Instance =>
        _instance ??= Resources.Load<GameConfigScriptableObject>("Config/棋盘数据");

    public bool bPlayerMoveFirst = true;
    public bool bCircleFirst = true;
    public bool bMCTS = false;
}