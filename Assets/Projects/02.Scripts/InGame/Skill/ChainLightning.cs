using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : SkillBase
{
    private readonly List<Enemy> hitEnemies = new List<Enemy>();

    public int ChainCount { get; set; } = 2;

    private const float MOVE_SPEED = 20f;
    private const float HIT_DISTANCE = 0.2f;

    private Vector3 lastPos;

    private void OnDisable()
    {
        hitEnemies.Clear();
    }

    public override void UseSkill()
    {
        ExecuteChainLightning();
        OnSkillImpact?.Invoke();
    }

    private void ExecuteChainLightning()
    {
        Vector3 currentPos = transform.position;

        for (int i = 0; i < ChainCount; i++)
        {
            Enemy target = enemyGenerator.GetClosetEnemy(currentPos, hitEnemies);
            if (target == null) break;

            float distance = Vector2.Distance(currentPos, target.Transform.position);
            if (distance > 3f) break;

            HitEnemy(target);
            CreateLightningEffect(currentPos, target.Transform.position);

            currentPos = target.Transform.position;
        }
    }

    private void HitEnemy(Enemy target)
    {
        combatManager.HandleDamage(Unit.Instance, target, 0.8f, ElementType.Lightning);
        hitEnemies.Add(target);
    }

    private void CreateLightningEffect(Vector3 start, Vector3 end)
    {
        Lightning lightning = effectGenerator.CreateAndGetPool<Lightning>(2);
        lightning.Setup(start, end);
        lightning.Activate();
    }

    public override void UpdateSkill()
    {
        Vector3 newPos = transform.position + transform.up * (MOVE_SPEED * Time.deltaTime);

        Enemy firstHit = enemyGenerator.GetClosetEnemy(transform.position);
        if (firstHit != null && !hitEnemies.Contains(firstHit))
        {
            float distance = DistanceFromPointToSegment(
                firstHit.Transform.position,
                lastPos,
                newPos
            );

            if (distance <= HIT_DISTANCE)
            {
                HitEnemy(firstHit);
                FinishSkill();
                return;
            }
        }

        transform.position = newPos;
        lastPos = newPos;

        if (IsOutsideViewport(transform.position))
        {
            FinishSkill();
        }
    }

    private bool IsOutsideViewport(Vector3 pos)
    {
        Vector3 s = mainCamera.WorldToViewportPoint(pos);
        return s.x < 0 || s.x > 1 || s.y < 0 || s.y > 1;
    }

    private void FinishSkill()
    {
        skillController.OnSkillUpdated -= UpdateSkill;
        UseSkill();
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
