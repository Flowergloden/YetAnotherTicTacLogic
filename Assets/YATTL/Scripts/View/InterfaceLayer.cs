using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InterfaceLayer : MonoBehaviour
{
    private LogicLayer LogicLayer => LogicLayer.Instance;
    private MapConfigScriptableObject MapConfig => MapConfigScriptableObject.Instance;

    private GameObject _map;
    private Dictionary<Vector2, GameObject> _generated = new();

    private void Start()
    {
        _map = new GameObject("Map");
        var delta = MapConfig.elementSize + MapConfig.splitSize;

        for (int x = 0; x < LogicLayer.MapData.Count; x++)
        {
            for (int y = 0; y < LogicLayer.MapData[0].Count; y++)
            {
                var obj = Instantiate(MapConfig.elements[(int)LogicLayer.MapData[x][y].Type],
                    new Vector3(x * delta, y * delta, 0),
                    Quaternion.identity, _map.transform);
                obj.transform.localScale = new Vector3(MapConfig.elementSize, MapConfig.elementSize, 1);
                _generated[new Vector2(x, y)] = obj;
            }
        }
    }

    private void Update()
    {
        while (LogicLayer.UpdateQueue.Any())
        {
            var pos = LogicLayer.UpdateQueue.Dequeue();
            Destroy(_generated[pos]);

            var delta = MapConfig.elementSize + MapConfig.splitSize;
            var x = (int)pos.x;
            var y = (int)pos.y;
            var obj = Instantiate(MapConfig.elements[(int)LogicLayer.MapData[x][y].Type],
                new Vector3(x * delta, y * delta, 0),
                Quaternion.identity, _map.transform);
            obj.transform.localScale = new Vector3(MapConfig.elementSize, MapConfig.elementSize, 1);
            _generated[new Vector2(x, y)] = obj;
        }
    }
}