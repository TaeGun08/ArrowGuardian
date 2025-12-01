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

    private Vector3 lastPos;

    protected virtual void Start()
    {
        mainCamera = Camera.main;
        combatManager = CombatManager.Instance;
        generatorManager = GeneratorManager.Instance;
        unit = Unit.Instance;

        lastPos = transform.position;
    }

    protected void Update()
    {
        Vector3 newPos = transform.position + transform.up * (arrowSpeed * Time.deltaTime);

        Enemy targetEnemy = generatorManager.EnemyGenerator.GetClosetEnemy(transform.position);
        if (targetEnemy != null)
        {
            float distance = DistanceFromPointToSegment(
                targetEnemy.Transform.position,
                lastPos,
                newPos
            );

            if (distance <= 0.2f)
            {
                CombatManager.Instance.HandleDamage(unit, targetEnemy, 1);
                InjectStatusEffect(elementType, unit, targetEnemy);
                if (statusEffects.Count != 0) combatManager.HandleApplyStatusEffect(statusEffects);

                OnArrowImpact?.Invoke();
                return;
            }
        }

        transform.position = newPos;
        lastPos = newPos;
    }

    protected float DistanceFromPointToSegment(Vector2 point, Vector2 a, Vector2 b)
    {
        Vector2 ab = b - a;
        float t = Vector2.Dot(point - a, ab) / ab.sqrMagnitude;
        t = Mathf.Clamp01(t);
        Vector2 closestPoint = a + t * ab;
        return Vector2.Distance(point, closestPoint);
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