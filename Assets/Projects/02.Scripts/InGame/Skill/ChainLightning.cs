using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : SkillBase
{
    private readonly List<Enemy> hitEnemies = new List<Enemy>();

    public int ChainCount { get; set; } = 2;

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

            float dist = Vector2.Distance(currentPos, target.Transform.position);
            if (dist > 0.5f) break;

            HitEnemy(target);

            CreateLightningEffect(target);

            currentPos = target.Transform.position;
        }
    }

    private void HitEnemy(Enemy target)
    {
        combatManager.HandleDamage(Unit.Instance, target, 1f, ElementType.Water);
        hitEnemies.Add(target);
    }

    private void CreateLightningEffect(Enemy target)
    {
        Lightning lightning = effectGenerator.CreateAndGetPool<Lightning>(2);
        lightning.Target = target;
        lightning.Activate();
    }

    public override void UpdateSkill()
    {
        transform.position += transform.up * (20f * Time.deltaTime);

        if (IsOutsideViewport(transform.position))
        {
            FinishSkill();
            return;
        }

        Enemy firstHit = enemyGenerator.GetClosetEnemy(transform.position);

        if (firstHit == null || hitEnemies.Contains(firstHit)) return;

        if (Vector2.Distance(transform.position, firstHit.Transform.position) > 0.2f) return;

        HitEnemy(firstHit);

        FinishSkill();
    }

    private bool IsOutsideViewport(Vector3 pos)
    {
        Vector3 screenPos = mainCamera.WorldToViewportPoint(pos);
        return screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1;
    }

    private void FinishSkill()
    {
        skillController.OnSkillUpdated -= UpdateSkill;
        UseSkill();
    }
}
