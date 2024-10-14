using UnityEngine;

public class InterfaceLayer : MonoBehaviour
{
    private LogicLayer LogicLayer => LogicLayer.Instance;
    private MapConfigScriptableObject MapConfig => MapConfigScriptableObject.Instance;

    private GameObject _map;

    private void Start()
    {
        _map = new GameObject("Map");
        var delta = MapConfig.elementSize + MapConfig.splitSize;

        for (int x = 0; x < LogicLayer.MapData.Count; x++)
        {
            for (int y = 0; y < LogicLayer.MapData[0].Count; y++)
            {
                var obj = Instantiate(MapConfig.elements[(int)MapData.Type],
                    new Vector3(x * delta, y * delta, 0),
                    Quaternion.identity, _map.transform);
                obj.transform.localScale = new Vector3(MapConfig.elementSize, MapConfig.elementSize, 1);
            }
        }
    }

    private void Update()
    {
    }
}