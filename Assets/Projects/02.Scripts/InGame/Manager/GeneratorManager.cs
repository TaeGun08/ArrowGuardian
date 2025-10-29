using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    [Header("Generator Settings")]
    [field: SerializeField] public ArrowGenerator ArrowGenerator { get; private set; }
    [field: SerializeField] public EnemyGenerator EnemyGenerator { get; private set; }
}
