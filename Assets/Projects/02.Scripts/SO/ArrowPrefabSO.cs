using UnityEngine;

[CreateAssetMenu(fileName = "ArrowPrefabSO", menuName = "Scriptable Objects/ArrowPrefabSO")]
public class ArrowPrefabSO : PrefabSoBase
{
    [Header("Arrows Settings")] 
    [SerializeField] private Arrow[] arrows;

    public override T GetPrefab<T>(int index)
    {
        foreach (var arrow in arrows)
        {
            if ((int)(arrow.ElementType) == index) return arrow as T; 
        }
        
        return arrows[0] as T;
    }
}
