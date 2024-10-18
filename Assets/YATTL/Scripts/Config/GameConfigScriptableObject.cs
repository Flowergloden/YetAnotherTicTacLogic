using UnityEngine;

public class GameConfigScriptableObject : ScriptableObject
{
    private static GameConfigScriptableObject _instance;

    public static GameConfigScriptableObject Instance =>
        _instance ??= Resources.Load<GameConfigScriptableObject>("Config/游戏配置");

    public bool bPlayerMoveFirst = true;
    public bool bCircleFirst = true;
}