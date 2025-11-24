using UnityEngine;

public class ChainLightningAbility : AbilityBase
{
    public override string AbilityName => "ChainLightning";
    public override string Description => "Summons a bolt of lightning that damages all nearby enemies.";

    private int chainCount;

    private float duration;
    private float timer;
    
    public override void Activate()
    {
        Debug.Log("ChainLightning");
        chainCount++;
        duration = 3f;
    }
    
    public override void UpdateAbility()
    {
        timer += Time.deltaTime;

        if (timer < duration) return;
        timer = 0;
        CastChainLightning();
    }
    
    private void CastChainLightning()
    {
        if (generatorManager.EnemyGenerator.EnemyListCheck()) return;
            
        ChainLightning chainLightning = skillGenerator.CreateAndGetPool<ChainLightning>(2);
        chainLightning.transform.position = unit.transform.position;
        if (chainLightning == null) return;
            
        Enemy enemy = GeneratorManager.Instance.EnemyGenerator.GetClosetEnemy(chainLightning.transform.position);
        Vector2 dir = (enemy.transform.position - chainLightning.transform.position).normalized;
            
        float baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
        Quaternion rotation = Quaternion.Euler(0, 0, baseAngle - 90f);
        chainLightning.transform.rotation = rotation;
            
        chainLightning.InitSkill();
        chainLightning.ChainCount = chainCount;
        chainLightning.OnSkillImpact += () =>
        {
            skillGenerator.ReturnPool(0, chainLightning);
            chainLightning.OnSkillImpact = null;
        };
    }

    public override void StackAbility()
    {
        chainCount++;
    }
}
