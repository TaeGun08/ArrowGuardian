using UnityEngine;

public class EffectGenerator : GeneratorBase
{
    private void Awake()
    {
        prefabSoBase = Resources.Load<EnemyPrefabSO>("EnemyPrefabSO");
        parentTransform = transform;
    }
}
