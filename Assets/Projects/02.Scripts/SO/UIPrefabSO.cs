using UnityEngine;

[CreateAssetMenu(fileName = "UIPrefabSO", menuName = "Scriptable Objects/UIPrefabSO")]
public class UIPrefabSO : ScriptableObject
{
    [System.Serializable]
    public class UIPrefab
    {
        public string Name;
        public GameObject Prefab;
    }
    
    [Header("UI Prefab")]
    [SerializeField] private UIPrefab[] uiPrefabs;

    public GameObject GetUIPrefab(string name)
    {
        for (var i = 0; i < uiPrefabs.Length; i++)
        {
            var uiPrefab = uiPrefabs[i];
            if (uiPrefab.Name == name) return uiPrefab.Prefab;
        }

        return null;
    }
}
