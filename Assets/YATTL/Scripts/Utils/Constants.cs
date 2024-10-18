namespace Constants
{
    public static class Game
    {
        public static bool bPlayerMoveFirst = true;
        public static bool bCircleFirst = true;
    }

    public static class Map
    {
        public static int MapSize = MapConfigScriptableObject.Instance.mapSize;
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