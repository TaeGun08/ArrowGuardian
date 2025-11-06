using UnityEngine;

public abstract class Enemy : MonoBehaviour, IElementType, IDamageAble, IMovement
{
    public IDamageAble Target => this;

    public GameObject GameObject => gameObject;
    public Transform Transform => transform;

    [Header("Enemy Settings")]
    [SerializeField] private ElementType elementType;
    public ElementType ElementType => elementType;

    protected EnemyData enemyData = new EnemyData();
    public EnemyData EnemyData => enemyData;
    
    public bool IsStop { get; set; }
    public float Speed { get; set; }
    
    protected virtual void Awake()
    {
        enemyData = EnemyLoaderCSV.GetEnemyByElementType(elementType);
        Speed = enemyData.MoveSpeed;
    }

    public virtual void TakeDamage(int damage)
    {
        enemyData.Health -= damage;
        if (enemyData.Health <= 0) Destroy(gameObject);
    }
}
