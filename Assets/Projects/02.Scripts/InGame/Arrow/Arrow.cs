using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arrow : MonoBehaviour, IElementType
{
    protected Camera mainCamera;
    protected CombatManager combatManager;

    [Header("Arrow Settings")] 
    [SerializeField] protected int damage;
    [SerializeField] protected ElementType elementType;

    public ElementType ElementType => elementType;
    [SerializeField] protected float arrowSpeed = 20f;

    private IDamageAble target;
    
    private List<IStatusEffect> statusEffects = new List<IStatusEffect>();

    protected virtual void Start()
    {
        mainCamera = Camera.main;
        combatManager = CombatManager.Instance;
    }

    protected void Update()
    {
        transform.position += transform.up * (arrowSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.Transform.position) > 0.1f) return;
        combatManager.HandleDamage(target, damage, elementType);
        InjectStatusEffect(elementType);
        
        //if(statusEffects.Count != 0) combatManager.HandleApplyStatusEffect(statusEffects);
        target = null;
        Destroy(gameObject);
    }

    protected void LateUpdate()
    {
        Vector3 screenPos = mainCamera.WorldToViewportPoint(transform.position);

        if (screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(IDamageAble target)
    {
        this.target = target;
    }
    
    protected void InjectStatusEffect(ElementType elementType)
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
        statusEffect.Target = target;
        statusEffects.Add(statusEffect);
        combatManager.statusEffectList.Add(statusEffect);
    }
}