using System.Collections.Generic;
using UnityEngine;

public class Fireball : SkillBase
{
    private const float MOVE_SPEED = 20f;
    private const float HIT_DISTANCE = 0.2f;
    
    public int FireballCount { get; set; }

    private float ExplosionRange => 0.4f + Mathf.Sqrt(FireballCount) * 0.2f;
    
    public override void UseSkill()
    {
        CreateExplosion();

        List<Enemy> enemies = enemyGenerator.GetEnemyList();
        float range = ExplosionRange + 0.2f;

        for (int i = 0; i < enemies.Count; i++)
        {
            Enemy enemy = enemies[i];
            if (enemy == null) continue;

            float dist = Vector2.Distance(transform.position, enemy.Transform.position);
            if (dist > range) continue;

            ApplyFireDamage(enemy, 1.5f);
        }
    }

    public override void UpdateSkill()
    {
        MoveForward();

        if (IsOutOfScreen())
        {
            EndSkill();
            return;
        }

        Enemy nearest = enemyGenerator.GetClosetEnemy(transform.position);
        if (nearest == null) return;

        if (Vector2.Distance(transform.position, nearest.Transform.position) > HIT_DISTANCE)
            return;

        ApplyFireDamage(nearest, 1f);
        UseSkill();
        EndSkill();
    }

    private void MoveForward()
    {
        transform.position += transform.up * (MOVE_SPEED * Time.deltaTime);
    }

    private bool IsOutOfScreen()
    {
        Vector3 view = mainCamera.WorldToViewportPoint(transform.position);
        return view.x < 0 || view.x > 1 || view.y < 0 || view.y > 1;
    }

    private void EndSkill()
    {
        skillController.OnSkillUpdated -= UpdateSkill;
        OnSkillImpact?.Invoke();
    }

    private void CreateExplosion()
    {
        Explosion explosion = effectGenerator.CreateAndGetPool<Explosion>(0);

        explosion.transform.position = transform.position;

        float scale = ExplosionRange * 2f;
        explosion.transform.localScale = Vector3.one * scale;

        explosion.Activate();
    }

    private void ApplyFireDamage(Enemy enemy, float damage)
    {
        combatManager.HandleDamage(Unit.Instance, enemy, damage, ElementType.Flame);
        Debuff<BurnEffect>(enemy);
    }
}
