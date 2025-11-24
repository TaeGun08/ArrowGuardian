using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour, ISkillAble
{
    protected CombatManager combatManager;
    protected SkillController skillController;
    protected EnemyGenerator enemyGenerator;
    protected EffectGenerator effectGenerator;
    protected Camera mainCamera;

    public Action OnSkillImpact { get; set;  }
    public float Cooldown { get; }
    
    public virtual void InitSkill()
    {
        combatManager = CombatManager.Instance;
        skillController = SkillController.Instance;
        enemyGenerator = GeneratorManager.Instance.EnemyGenerator;
        effectGenerator = GeneratorManager.Instance.EffectGenerator;
        mainCamera = Camera.main;
        skillController.OnSkillUpdated += UpdateSkill;
    }

    public abstract void UseSkill();

    public abstract void UpdateSkill();
    
    protected void Debuff<T>(Enemy targetEnemy) where T : IStatusEffect, new()
    {
        List<IStatusEffect> statusEffects = new List<IStatusEffect>();
        IStatusEffect statusEffect = StatusEffectFactory.CreateStatusEffect<T>();
        statusEffect.Sender = Unit.Instance;
        statusEffect.Target = targetEnemy;
        statusEffects.Add(statusEffect);
        combatManager.HandleApplyStatusEffect(statusEffects);
    }
}
