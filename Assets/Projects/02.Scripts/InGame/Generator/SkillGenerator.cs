using System;
using UnityEngine;

public sealed class SkillGenerator : GeneratorBase
{
    private void Awake()
    {
        prefabSoBase = Resources.Load<SkillPrefabSO>("SkillPrefabSO");
        parentTransform = transform;
    }
}
