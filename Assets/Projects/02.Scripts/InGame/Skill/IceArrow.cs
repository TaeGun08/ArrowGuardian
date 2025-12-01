using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IceArrow : SkillBase
{
    private List<Enemy> hitEnemies = new List<Enemy>();
    public int IceArrowCount { get; set; }
    private int targetCount;

    private const float MOVE_SPEED = 20f;
    private const float HIT_DISTANCE = 0.2f;

    private Vector3 lastPos;

    private void OnDisable()
    {
        hitEnemies.Clear();
        targetCount = 0;
    }

    public override void UseSkill()
    {
        if (Random.value > 0.5f) return;
        if (hitEnemies.Count == 0 || targetCount >= hitEnemies.Count) return;

        Enemy enemy = hitEnemies[targetCount];
        Debuff<FreezeEffect>(enemy);
    }

    public override void UpdateSkill()
    {
        Vector3 newPos = transform.position + transform.up * (MOVE_SPEED * Time.deltaTime);
        
        Enemy targetEnemy = enemyGenerator.GetClosetEnemy(transform.position);
        if (targetEnemy != null && !CheckEnemyList(targetEnemy))
        {
            float distance = DistanceFromPointToSegment(
                targetEnemy.Transform.position,
                lastPos,
                newPos
            );

            if (distance <= HIT_DISTANCE)
            {
                combatManager.HandleDamage(Unit.Instance, targetEnemy, 1f, ElementType.Water);

                hitEnemies.Add(targetEnemy);
                Debuff<SlowEffect>(targetEnemy);

                UseSkill();
                targetCount++;

                if (IceArrowCount <= targetCount)
                {
                    OnSkillImpact?.Invoke();
                    skillController.OnSkillUpdated -= UpdateSkill;
                    return;
                }
            }
        }
        
        transform.position = newPos;
        lastPos = newPos;
        
        Vector3 screenPos = mainCamera.WorldToViewportPoint(transform.position);
        if (screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1)
        {
            skillController.OnSkillUpdated -= UpdateSkill;
            OnSkillImpact?.Invoke();
        }
    }

    private bool CheckEnemyList(Enemy compare)
    {
        if (compare == null) return true;

        for (int i = 0; i < hitEnemies.Count; i++)
        {
            if (hitEnemies[i] == compare) return true;
        }

        return false;
    }

    private float DistanceFromPointToSegment(Vector2 point, Vector2 a, Vector2 b)
    {
        Vector2 ab = b - a;
        float t = Vector2.Dot(point - a, ab) / ab.sqrMagnitude;
        t = Mathf.Clamp01(t);
        Vector2 closestPoint = a + t * ab;
        return Vector2.Distance(point, closestPoint);
    }
}
