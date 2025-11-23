using UnityEngine;

public class EffectGenerator : GeneratorBase
{
    private void Awake()
    {
        prefabSoBase = Resources.Load<EffectPrefabsSO>("EffectPrefabsSO");
        parentTransform = transform;
    }
}
