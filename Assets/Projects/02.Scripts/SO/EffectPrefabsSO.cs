using UnityEngine;

[CreateAssetMenu(fileName = "EffectPrefabsSO", menuName = "Scriptable Objects/EffectPrefabsSO")]
public class EffectPrefabsSO : PrefabSoBase
{
    [Header("EffectPrefabs")]
    [SerializeField] private EffectBase[] effectPrefabs;

    public override T GetPrefab<T>(int index)
    {
        foreach (var effect in effectPrefabs)
        {
            if (effect.Id == index) return effect as T; 
        }
        
        return effectPrefabs[0] as T;
    }
}
