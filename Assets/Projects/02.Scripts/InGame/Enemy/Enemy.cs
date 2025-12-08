using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IElementType, IDamageAble, IMovement, IRunTimeStats
{
    public GameObject GameObject => gameObject;
    public Transform Transform => transform;
    public IRunTimeStats runTimeStats => this;
    public IMovement IMovement => this;

    [Header("Enemy Settings")]
    [SerializeField] private ElementType elementType;
    public ElementType ElementType => elementType;

    protected EnemyData enemyData = new EnemyData();
    
    public bool IsStop { get; set; }
    public bool IsStunned { get; set; }
    public float Speed { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }
    public int Armor { get; set; }
    
    public Action OnDeath { get; set; }

    private float timer;
    private float duration;
    
    protected virtual void Awake()
    {
        enemyData = CSVLoader.LoadById<EnemyData>("EnemyData", (int)elementType);
    }

    private void OnEnable()
    {
        Speed = enemyData.MoveSpeed;
        Health = enemyData.MaxHealth;
        Armor = enemyData.Armor;
        IsStop = false;
        IsStunned = false;
        timer = 0;
        
        CombatManager.Instance.OnEnemyCombatEvent += Attack;
    }

    private void OnDisable()
    {
        CombatManager.Instance.OnEnemyCombatEvent -= Attack;
    }

    private void Attack()
    {
        if (IsStop == false) return;
        
        timer += Time.deltaTime;
        if (timer < enemyData.AttackDelay) return;
        GameManager.Instance.SetHealth(enemyData.Damage);
        timer = 0f;
    }

    public virtual int TakeDamage(int damage)
    {
        int sumDamage = damage - Armor;
        if (sumDamage <= 0) sumDamage = 1;
        Health -= sumDamage;
        
        if (Health <= 0) OnDeath?.Invoke();
        
        return sumDamage;
    }
}
