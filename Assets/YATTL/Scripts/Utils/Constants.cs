namespace Constants
{
    public static class Game
    {
        public static bool bPlayerMoveFirst = true;
        public static bool bCircleFirst = true;
    }

    public static class Map
    {
        private static MapConfigScriptableObject MapConfig => MapConfigScriptableObject.Instance;
        public static readonly int MapSize = MapConfig.mapSize;
        public static readonly float ElementSize = MapConfig.elementSize;
        public static readonly float SplitSize = MapConfig.splitSize;
    }

    public static class Minimax
    {
        public static int Rules = 4;
        public static float Win = float.MaxValue;
        public static float OneStep2Win = 1;
        public static float TwoSteps2Win = 5;
        public static float TreeSteps2Win = 10;
    }
}