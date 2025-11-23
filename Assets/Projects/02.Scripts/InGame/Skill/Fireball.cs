using System.Collections.Generic;
using UnityEngine;

public class Fireball : SkillBase
{
    public override void UseSkill()
    {
        ExplosionEffect explosionEffect = effectGenerator.CreateAndGetPool<ExplosionEffect>(0);
        explosionEffect.transform.position = transform.position;
        explosionEffect.Activate();
        
        for (int i = 0; i < enemyGenerator.GetEnemyList().Count; i++)
        {
            Enemy targetEnemy = enemyGenerator.GetClosetEnemy(transform.position);
            
            if (Vector2.Distance(transform.position, targetEnemy.Transform.position) > 0.8f) continue;
            
            combatManager.HandleDamage(Unit.Instance, targetEnemy, 1.5f, ElementType.Flame);
            
            List<IStatusEffect> statusEffects = new List<IStatusEffect>();
            IStatusEffect statusEffect = StatusEffectFactory.CreateStatusEffect<BurnEffect>();
            statusEffect.Target = targetEnemy;
            statusEffects.Add(statusEffect);
            combatManager.HandleApplyStatusEffect(statusEffects);
        }
    }

    public override void UpdateSkill()
    {
        transform.position += transform.up * (20f * Time.deltaTime);
        
        Vector3 screenPos = mainCamera.WorldToViewportPoint(transform.position);

        if (screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1)
        {
            skillController.OnSkillUpdated -= UpdateSkill;
            OnSkillImpact?.Invoke();
        }
        
        Enemy targetEnemy = enemyGenerator.GetClosetEnemy(transform.position);
        
        if (targetEnemy == null) return;
        
        if (Vector2.Distance(transform.position, targetEnemy.Transform.position) > 0.2f) return;
        
        combatManager.HandleDamage(Unit.Instance, targetEnemy, 1f, ElementType.Flame);

        UseSkill();
        
        OnSkillImpact?.Invoke();
        skillController.OnSkillUpdated -= UpdateSkill;
    }
}
