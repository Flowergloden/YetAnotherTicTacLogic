using System;
using UnityEngine;

public class PlacementButtonDrawer : MonoBehaviour
{
    private static MapConfigScriptableObject MapConfig => MapConfigScriptableObject.Instance;
    private static LogicLayer LogicLayer => LogicLayer.Instance;
    private static LogicLayerUpdater LogicLayerUpdater => LogicLayerUpdater.Instance;
    private static InterfaceLayer InterfaceLayer => InterfaceLayer.Instance;
    private static GameManager GameManager => GameManager.Instance;

    private static Camera _mainCamera;

    private GUIStyle _style;

    private void Start()
    {
        var tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.clear);
        tex.Apply();
        _mainCamera = Camera.main;
        _style = new GUIStyle
        {
            normal = new GUIStyleState()
            {
                background = tex,
            },
        };
    }

    private void OnGUI()
    {
        if (!GameManager.bPlayerMove)
        {
            return;
        }

        for (int x = 0; x < LogicLayer.MapData.Count; x++)
        {
            for (int y = 0; y < LogicLayer.MapData[0].Count; y++)
            {
                if (LogicLayer.MapData[x][y].Type == MapElementType.None)
                {
                    var pos = new Vector2(x, y);
                    var guiPos = new Vector2(x, MapConfig.mapSize - y - 1);
                    var worldPos = InterfaceLayer.Generated[guiPos].transform.position;

                    var leftTop = _mainCamera.WorldToScreenPoint(new Vector3(worldPos.x - MapConfig.elementSize / 2,
                        worldPos.y - MapConfig.elementSize / 2, 0));
                    var rightBottom = _mainCamera.WorldToScreenPoint(new Vector3(worldPos.x + MapConfig.elementSize / 2,
                        worldPos.y + MapConfig.elementSize / 2, 0));

                    var size = rightBottom - leftTop;

                    // var rect = new Rect(leftTop.x, leftTop.y, size.x, size.y);
                    var rect = new Rect(leftTop.x, leftTop.y, size.x, size.y);

                    if (GUI.Button(rect, GUIContent.none, _style))
                    {
                        LogicLayerUpdater.Update(pos,
                            new MapData { Type = GameManager.bCross ? MapElementType.Cross : MapElementType.Circle });
                        GameManager.bPlayerMove = !GameManager.bPlayerMove;
                    }
                }
            }
        }
    }
}