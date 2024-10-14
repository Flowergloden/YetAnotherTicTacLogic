using UnityEditor;
using UnityEngine;

public class TmpPlaceTool : EditorWindow
{
    [MenuItem("Tools/临时放置工具")]
    public static void ShowWindow()
    {
        GetWindow<TmpPlaceTool>("临时放置工具");
    }

    private Vector2 _pos;
    private MapElementType _type;

    private void OnGUI()
    {
        _pos = EditorGUILayout.Vector2Field("Position", _pos);
        _type = (MapElementType)EditorGUILayout.EnumPopup("Type", _type);

        if (GUILayout.Button("Place"))
        {
            LogicLayerUpdater.Instance.Update(_pos, new MapData { Type = _type });
        }
    }
}