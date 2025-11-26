using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IElementType, IDamageAble, IMovement, IRunTimeStats
{
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");

    public enum AnimState
    {
        Idle,
        Walking,
    }
    
    public GameObject GameObject => gameObject;
    public Transform Transform => transform;
    public IRunTimeStats runTimeStats => this;
    public IMovement IMovement => this;

    [Header("Enemy Settings")]
    [SerializeField] private ElementType elementType;
    public ElementType ElementType => elementType;

    protected EnemyData enemyData = new EnemyData();
    
    public bool IsStop { get; set; }
    public float Speed { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }
    public int Armor { get; set; }
    
    public Action OnDeath { get; set; }
    
    protected Animator animator;
    
    protected virtual void Awake()
    {
        enemyData = EnemyLoaderCSV.GetEnemyByElementType(elementType);
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        Speed = enemyData.MoveSpeed;
        Health = enemyData.MaxHealth;
        Armor = enemyData.Armor;
        IsStop = false;
        ChangeAnimation(AnimState.Walking);
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage - Armor;
        if (Health <= 0) OnDeath?.Invoke();
    }

    public void ChangeAnimation(AnimState state)
    {
        switch (state)
        {
            case AnimState.Idle:
                animator.SetTrigger(Idle);
                break;
            case AnimState.Walking:
                Debug.Log(animator);
                animator.SetTrigger(Walk);
                break;
        }
    }
}
