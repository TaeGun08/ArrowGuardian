using System;
using UnityEngine;
using Object = System.Object;

public sealed class ArrowGenerator : GeneratorBase
{
    private void Awake()
    {
        prefabSoBase = Resources.Load<ArrowPrefabSO>("ArrowPrefabSO");
        parentTransform = transform;
    }
}
