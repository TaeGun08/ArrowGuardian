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
        if(statusEffects.Count != 0) combatManager.HandleApplyStatusEffect(statusEffects);
        
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

    private List<IStatusEffect> statusEffects = new List<IStatusEffect>();
    
    public void InjectStatusEffect(IStatusEffect statusEffect)
    {
        statusEffects.Add(statusEffect);
    }
    
}