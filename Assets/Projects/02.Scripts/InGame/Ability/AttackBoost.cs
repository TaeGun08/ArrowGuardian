 using UnityEngine;

public class AttackBoost : AbilityBase
{
    public override string AbilityName => "AttackBoost";
    public override string Description => "Increases attack power by 60% for a short duration.";
    
    private int sumDamage;
    
    public override void Activate()
    {
        Debug.Log("AttackBoost");
        sumDamage = unit.Damage + (int)(unit.UnitData.Damage * 0.6f);
        unit.Damage += sumDamage;
    }

    public override void StackAbility()
    {
        Debug.Log("Stack AttackBoost");
        unit.Damage += sumDamage;
    }
}
