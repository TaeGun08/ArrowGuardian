using UnityEngine;

public class GeneratorManager : SingletonBase<GeneratorManager>
{
    [Header("Generator Settings")]
    [field: SerializeField] public ArrowGenerator ArrowGenerator { get; private set; }
    [field: SerializeField] public EnemyGenerator EnemyGenerator { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        ArrowGenerator = GetComponentInChildren<ArrowGenerator>();
        EnemyGenerator = GetComponentInChildren<EnemyGenerator>();
    }
}
