using UnityEngine;

public abstract class PrefabSoBase : ScriptableObject
{
    public abstract T GetPrefab<T>(int index) where T : MonoBehaviour;
}
