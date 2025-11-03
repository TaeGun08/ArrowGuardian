using UnityEngine;

public abstract class Enemy : MonoBehaviour, IElementType, IDamageAble, IBuffable, IDeBuffable
{
    public IDamageAble Target => this;

    public GameObject GameObject => gameObject;
    public Transform Transform => transform;

    [Header("Enemy Settings")]
    [SerializeField] private ElementType elementType;
    public ElementType ElementType => elementType;

    protected EnemyDataSO enemySo;
    protected EnemyData enemyData = new EnemyData();
    
    protected virtual void Awake()
    {
        enemySo = Resources.Load<EnemyDataSO>("EnemyDataSO");
        enemyData = enemySo.GetEnemyData((int)elementType);
    }

    public virtual void TakeDamage(int damage)
    {
        enemyData.Health -= damage;
        if (enemyData.Health <= 0) Destroy(gameObject);
    }
    
    public void ApplyDeBuff(DeBuff debuff)
    {
        
    }

    public void ApplyBuff(Buff buff)
    {
        
    }
}
