using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public GameManager instance;

    private void Awake()
    {
        instance = this;

        LogicLayerUpdater.Instance.Initialize();
        gameObject.AddComponent<InterfaceLayer>();
    }
}