using UnityEditor;
using UnityEngine;

public abstract class ItemDataSO : ScriptableObject
{
    public string ID;
    public string itemName;
    public string description;
    public GameObject gameObjectPrefab;
}
