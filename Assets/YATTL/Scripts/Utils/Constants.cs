using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

namespace Constants
{
    public static class Game
    {
        private static GameConfigScriptableObject GameConfig => GameConfigScriptableObject.Instance;
        public static bool bPlayerMoveFirst = GameConfig.bPlayerMoveFirst;
        public static bool bCircleFirst = GameConfig.bCircleFirst;
    }

    public static class Map
    {
        private static MapConfigScriptableObject MapConfig => MapConfigScriptableObject.Instance;
        public static readonly int MapSize = MapConfig.mapSize;
        public static readonly float ElementSize = MapConfig.elementSize;
        public static readonly float SplitSize = MapConfig.splitSize;
    }

    [Serializable]
    public class SearchData
    {
        public virtual bool bMCTS => false;
        public Difficulty difficulty;
    }

    [Serializable]
    public class MinimaxData : SearchData
    {
        public override bool bMCTS => false;

        public int maxDepth;
        public float win = 999;
        public float oneStep2Win = 1;
        public float twoSteps2Win = 5;
        public float treeSteps2Win = 10;
    }

    [Serializable]
    public class MCTSData : SearchData
    {
        public override bool bMCTS => true;

        public float c;
        public int maxDepth;
        public int gamesPerSimulation;
        public int maxIteration;
    }

    public static class AI
    {
        private static AIConfigScriptableObject AIConfig => AIConfigScriptableObject.Instance;

        public static Dictionary<Difficulty, SearchData> Cfg = AIConfig.mctsConfig
            .ConvertAll<SearchData>(data => data)
            .Concat(AIConfig.minimaxConfig.ConvertAll<SearchData>(data => data))
            .ToDictionary(data => data.difficulty);
    }

    public static class Minimax
    {
        public static float Win = float.MaxValue;
        public static float OneStep2Win = 1;
        public static float TwoSteps2Win = 5;
        public static float TreeSteps2Win = 10;
    }
}