using UnityEngine;

public class IceArrowAbility : AbilityBase
{
    public override string AbilityName => "IceArrow";
    public override string Description => "Fires a freezing arrow that slows enemies on hit.";
    
    private int iceArrowCount;
    
    private float duration;
    private float timer;
    
    public override void Activate()
    {
        Debug.Log("IceArrow");
        iceArrowCount++;
        duration = 2f;
    }
    
    public override void UpdateAbility()
    {
        timer += Time.deltaTime;

        if (timer < duration) return;
        timer = 0;
        CasteIceArrow();
    }

    private void CasteIceArrow()
    {
        if (generatorManager.EnemyGenerator.EnemyListCheck()) return;
            
        IceArrow iceArrow = skillGenerator.CreateAndGetPool<IceArrow>(1);
        iceArrow.transform.position = unit.transform.position;
        if (iceArrow == null) return;
            
        Enemy enemy = GeneratorManager.Instance.EnemyGenerator.GetClosetEnemy(iceArrow.transform.position);
        Vector2 dir = (enemy.transform.position - iceArrow.transform.position).normalized;
            
        float baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
        Quaternion rotation = Quaternion.Euler(0, 0, baseAngle - 90f);
        iceArrow.transform.rotation = rotation;
            
        iceArrow.InitSkill();
        iceArrow.IceArrowCount = iceArrowCount;
        iceArrow.OnSkillImpact += () =>
        {
            skillGenerator.ReturnPool(0, iceArrow);
            iceArrow.OnSkillImpact = null;
        };
    }

    public override void StackAbility()
    {
        iceArrowCount++;
    }
}
