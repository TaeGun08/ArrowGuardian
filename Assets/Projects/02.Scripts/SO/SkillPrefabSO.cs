using UnityEngine;

[CreateAssetMenu(fileName = "SkillPrefabSO", menuName = "Scriptable Objects/SkillPrefabSO")]
public class SkillPrefabSO : PrefabSoBase
{
    [Header("SkillPrefab Settings")]
    [SerializeField] private SkillBase[] skillPrefabs;

    public override T GetPrefab<T>(int index)
    {
        for (int i = 0; i < skillPrefabs.Length; i++)
        {
            if (skillPrefabs[i] is T t)
                return t;
        }

        return null;
    }
}
