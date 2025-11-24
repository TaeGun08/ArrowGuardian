using System.Collections.Generic;
using UnityEngine;

public class Fireball : SkillBase
{
    public int FireballCount { get; set; }

    private float ExplosionRange()
    {
        return 0.4f + Mathf.Sqrt(FireballCount) * 0.2f;
    }
    
    public override void UseSkill()
    {
        Explosion explosion = effectGenerator.CreateAndGetPool<Explosion>(0);
        explosion.transform.position = transform.position;
        float scale = ExplosionRange() * 2f;
        explosion.transform.localScale = new Vector3(scale, scale, scale);
        explosion.Activate();

        List<Enemy> enemies = enemyGenerator.GetEnemyList();
        
        for (int i = 0; i < enemies.Count; i++)
        {
            Enemy targetEnemy = enemies[i];

            if (targetEnemy == null) continue;
            
            float distance = Vector2.Distance(transform.position, targetEnemy.Transform.position);
            if (distance > ExplosionRange()) continue;

            combatManager.HandleDamage(Unit.Instance, targetEnemy, 1.5f, ElementType.Flame);

            Debuff<BurnEffect>(targetEnemy);
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

        Debuff<BurnEffect>(targetEnemy);
        
        UseSkill();

        OnSkillImpact?.Invoke();
        skillController.OnSkillUpdated -= UpdateSkill;
    }
}