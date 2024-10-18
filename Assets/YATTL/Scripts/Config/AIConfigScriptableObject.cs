using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AIConfigScriptableObject : ScriptableObject
{
    private static AIConfigScriptableObject _instance;

    public static AIConfigScriptableObject Instance =>
        _instance ??= Resources.Load<AIConfigScriptableObject>("Config/AI配置");

    public List<Constants.MinimaxData> minimaxConfig;
    public List<Constants.MCTSData> mctsConfig;
}