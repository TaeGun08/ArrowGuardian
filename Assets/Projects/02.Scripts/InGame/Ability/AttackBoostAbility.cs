 using UnityEngine;

public class AttackBoostAbility : AbilityBase
{
    public override int Id => 0;
    public override string AbilityName => "AttackBoost";
    public override string Description => "Increases attack power by 60% for a short duration.";
    
    private int sumDamage;
    
    public override void Activate()
    {
        sumDamage = (int)(unit.UnitData.Damage * 0.6f);
        unit.Damage += sumDamage;
        Debug.Log($"AttackBoost ::: {unit.Damage}");
    }

    public override void StackAbility()
    {
        unit.Damage += sumDamage;
        Debug.Log($"AttackBoost ::: {unit.Damage}");
    }
}
