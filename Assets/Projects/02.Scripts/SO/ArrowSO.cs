using UnityEngine;

[CreateAssetMenu(fileName = "ArrowSO", menuName = "Scriptable Objects/ArrowSO")]
public class ArrowSO : ScriptableObject
{
    [Header("Arrows Settings")] 
    [SerializeField] private Arrow[] arrows;

    public Arrow GetArrow(int elementType)
    {
        foreach (var arrow in arrows)
        {
            if ((int)(arrow.ElementType) == elementType) return arrow; 
        }
        
        return arrows[0];
    }
}
