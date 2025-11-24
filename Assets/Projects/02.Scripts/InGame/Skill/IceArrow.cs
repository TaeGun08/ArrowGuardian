using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IceArrow : SkillBase
{
    private List<Enemy> hitEnemies = new List<Enemy>();

    public int IceArrowCount { get; set; }
    private int targetCount;

    private void OnDisable()
    {
        hitEnemies.Clear();
        targetCount = 0;
    }

    public override void UseSkill()
    {
        if (Random.value > 0.5f) return;
        Enemy enemy = hitEnemies[targetCount];

        Debuff<FreezeEffect>(enemy);
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

        if (targetEnemy == null || CheckEnemyList(targetEnemy)) return;

        if (Vector2.Distance(transform.position, targetEnemy.Transform.position) > 0.2f) return;

        combatManager.HandleDamage(Unit.Instance, targetEnemy, 1f, ElementType.Water);

        hitEnemies.Add(targetEnemy);

        Debuff<SlowEffect>(targetEnemy);

        UseSkill();

        targetCount++;

        if (IceArrowCount > targetCount) return;

        OnSkillImpact?.Invoke();
        skillController.OnSkillUpdated -= UpdateSkill;
    }

    private bool CheckEnemyList(Enemy compare)
    {
        if (compare == null) return true;

        int count = hitEnemies.Count;
        for (int i = 0; i < count; i++)
        {
            Enemy enemy = hitEnemies[i];
            if (enemy == compare) return true;
        }

        return false;
    }
}