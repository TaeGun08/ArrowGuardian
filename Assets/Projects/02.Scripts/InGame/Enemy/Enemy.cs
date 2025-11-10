using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IElementType, IDamageAble, IMovement, IRunTimeStatus
{
    public GameObject GameObject => gameObject;
    public Transform Transform => transform;
    public IRunTimeStatus IRunTimeStatus => this;
    public IMovement IMovement => this;

    [Header("Enemy Settings")]
    [SerializeField] private ElementType elementType;
    public ElementType ElementType => elementType;

    protected EnemyData enemyData = new EnemyData();
    public EnemyData EnemyData => enemyData;
    
    public bool IsStop { get; set; }
    public float Speed { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }
    public int Armor { get; set; }
    
    public Action OnDeath { get; set; }
    
    protected virtual void Awake()
    {
        enemyData = EnemyLoaderCSV.GetEnemyByElementType(elementType);
        Speed = enemyData.MoveSpeed;
        Health = enemyData.MaxHealth;
        Armor = enemyData.Armor;
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage - Armor;
        if (Health <= 0) OnDeath?.Invoke();
    }
}
