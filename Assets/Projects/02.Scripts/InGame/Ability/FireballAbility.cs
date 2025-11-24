using System.Collections.Generic;
using UnityEngine;

public class FireballAbility : AbilityBase
{
    public override string AbilityName => "Fireball";
    public override string Description => "Launches a blazing fireball that explodes on impact.";

    private int fireballCount;

    private float duration;
    private float timer;

    public override void Activate()
    {
        Debug.Log("Fireball");
        fireballCount++;
        duration = 3f;
    }

    public override void UpdateAbility()
    {
        timer += Time.deltaTime;

        if (timer < duration) return;
        timer = 0;
        CastFireball();
    }

    private void CastFireball()
    {
        if (generatorManager.EnemyGenerator.EnemyListCheck()) return;
            
        Fireball fireball = skillGenerator.CreateAndGetPool<Fireball>(0);
        fireball.transform.position = unit.transform.position;
        if (fireball == null) return;
            
        Enemy enemy = GeneratorManager.Instance.EnemyGenerator.GetClosetEnemy(fireball.transform.position);
        Vector2 dir = (enemy.transform.position - fireball.transform.position).normalized;
            
        float baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
        Quaternion rotation = Quaternion.Euler(0, 0, baseAngle - 90f);
        fireball.transform.rotation = rotation;
            
        fireball.InitSkill();
        fireball.FireballCount = fireballCount;
        fireball.OnSkillImpact += () =>
        {
            skillGenerator.ReturnPool(0, fireball);
            fireball.OnSkillImpact = null;
        };
    }
    
    public override void StackAbility()
    {
        fireballCount++;
    }
}