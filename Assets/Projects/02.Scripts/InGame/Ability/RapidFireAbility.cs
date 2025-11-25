using UnityEngine;

public class RapidFireAbility : AbilityBase
{
    public override string AbilityName => "RapidFire";
    public override string Description => "Fires continuously in rapid succession but lowers attack power.";

    private int diffDamage;
    
    public override void Activate()
    {
        unit.RapidFireCount += 1;
        diffDamage = (int)(unit.UnitData.Damage * 0.3f);
        unit.Damage -= diffDamage;
        Debug.Log($"RapidFire ::: {unit.RapidFireCount}, DamageDown ::: {unit.Damage}");
    }
 
    public override void StackAbility()
    {
        unit.RapidFireCount += 1;
        unit.Damage -= diffDamage;
        Debug.Log($"RapidFire ::: {unit.RapidFireCount}, DamageDown ::: {unit.Damage}");
    }
}