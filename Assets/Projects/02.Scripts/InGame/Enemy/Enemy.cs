using UnityEngine;

public abstract class Enemy : MonoBehaviour, IElementType, IDamageAble
{
    public GameObject GameObject => gameObject;
    
    [Header("Enemy Settings")]
    [SerializeField] private ElementType elementType;
    public ElementType ElementType => elementType;

    protected EnemyDataSO enemySo = new EnemyDataSO();
    protected EnemyData enemyData = new EnemyData();
    
    protected virtual void Awake()
    {
        enemySo = Resources.Load<EnemyDataSO>("EnemySO");
        enemyData = enemySo.GetEnemyData((int)elementType);
    }

    public void TakeDamage(int damage)
    {
        enemyData.Health -= damage;
        if (enemyData.Health <= 0) Destroy(gameObject);
    }
}
