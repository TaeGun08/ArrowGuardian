using System;
using UnityEngine;

public class SkillGenerator : GeneratorBase
{
    private void Awake()
    {
        prefabSoBase = Resources.Load<SkillPrefabSO>("SkillPrefabSO");
        parentTransform = transform;
    }
}
