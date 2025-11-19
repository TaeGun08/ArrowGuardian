using UnityEngine;

[CreateAssetMenu(fileName = "UIPrefabSO", menuName = "Scriptable Objects/UIPrefabSO")]
public class UIPrefabSO : PrefabSoBase
{
    [Header("UI Prefab")]
    [SerializeField] private ActionUI[] actionUIs;
    
    public override T GetPrefab<T>(int index)
    {
        if (index < 0 || index >= actionUIs.Length)
            return null;
        
        return actionUIs[index] as T;
    }
}
