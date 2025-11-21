using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arrow : MonoBehaviour, IElementType
{
    protected Camera mainCamera;
    protected CombatManager combatManager;
    protected GeneratorManager generatorManager;
    protected Unit unit;

    [Header("Arrow Settings")] 
    [SerializeField] protected ElementType elementType;

    public ElementType ElementType => elementType;
    [SerializeField] protected float arrowSpeed = 20f;
    
    private List<IStatusEffect> statusEffects = new List<IStatusEffect>();

    public Action OnArrowImpact { get; set; }

    protected virtual void Start()
    {
        mainCamera = Camera.main;
        combatManager = CombatManager.Instance;
        generatorManager =GeneratorManager.Instance;
        unit = Unit.Instance;
    }

    protected void Update()
    {
        transform.position += transform.up * (arrowSpeed * Time.deltaTime);

        Enemy targetEnemy = generatorManager.EnemyGenerator.GetClosetEnemy(transform.position);
        
        if (targetEnemy == null) return;
        if (Vector2.Distance(transform.position, targetEnemy.Transform.position) > 0.2f) return;
        CombatManager.Instance.HandleDamage(unit, targetEnemy, 1);
        InjectStatusEffect(elementType, unit, targetEnemy);
        
        if(statusEffects.Count != 0) combatManager.HandleApplyStatusEffect(statusEffects);
        OnArrowImpact?.Invoke();
    }

    protected void LateUpdate()
    {
        Vector3 screenPos = mainCamera.WorldToViewportPoint(transform.position);

        if (screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1)
        {
            OnArrowImpact?.Invoke();
        }
    }
    
    protected void InjectStatusEffect(ElementType elementType, IDamageAble sender, IDamageAble target)
    {
        IStatusEffect statusEffect = null;
        switch (elementType)
        {
            case ElementType.None:
                break;
            case ElementType.Flame:
                statusEffect = StatusEffectFactory.CreateStatusEffect<BurnEffect>();
                break;
            case ElementType.Water:
                statusEffect = StatusEffectFactory.CreateStatusEffect<SlowEffect>();
                break;
            case ElementType.Wind:
                break;
            case ElementType.Earth:
                break;
            case ElementType.Lightning:
                break;
            case ElementType.Dark:
                break;
            case ElementType.Light:
                break;
        }
        
        if (statusEffect == null) return;
        statusEffect.Sender = sender;
        statusEffect.Target = target;
        statusEffects.Add(statusEffect);
    }
}