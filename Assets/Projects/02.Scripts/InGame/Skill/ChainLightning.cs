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
        Vector3 s = mainCamera.WorldToViewportPoint(pos);
        return s.x < 0 || s.x > 1 || s.y < 0 || s.y > 1;
    }

    private void FinishSkill()
    {
        skillController.OnSkillUpdated -= UpdateSkill;
        UseSkill();
    }
}
