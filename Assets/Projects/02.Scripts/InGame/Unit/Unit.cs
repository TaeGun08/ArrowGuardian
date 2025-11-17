using UnityEngine;

public abstract class Unit : SingletonBase<Unit>, IDamageAble, IRunTimeStats
{
    public enum UnitName
    {
        ApprenticeArcher,
        EmberArcher,
        TidalArcher,
        GaleArcher,
        StoneArcher,
        StormArcher,
        ShadowArcher,
        RadiantArcher,
    }
    
    [Header("Unit Settings")]
    [SerializeField] private UnitName unitName;
    
    public ArrowPrefabSO ArrowPrefabSo { get; private set; }
    public UnitData UnitData { get; private set; }

    public ElementType ElementType { get; private set;}
    public GameObject GameObject => gameObject;
    public Transform Transform => transform;
    public IRunTimeStats runTimeStats => this;
    public IMovement IMovement { get; }
    
    public int Health { get; set; }
    public int Damage { get; set; }
    public int Armor { get; set; }

    public float AttackDelay { get; set; }
    public int MultiShotCount { get; set; }
    public int RapidFireCount { get; set; }
    
    protected override void Awake()
    {
        base.Awake();
        ArrowPrefabSo = Resources.Load<ArrowPrefabSO>("ArrowPrefabSO");
        
        UnitData = UnitLoaderCSV.GetUnitByName(unitName.ToString());

        ElementType = UnitData.ElementType;
        Damage = UnitData.Damage;
        AttackDelay = UnitData.AttackDelay;
        MultiShotCount = UnitData.MultiShotCount;
        RapidFireCount = UnitData.RapidFireCount;
    }
    
    protected void OnDrawGizmos()
    {
        if (UnitData == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, UnitData.Range);
    }
    
    public void TakeDamage(int damage)
    {
    }
}
