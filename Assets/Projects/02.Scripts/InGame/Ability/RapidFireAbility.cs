using UnityEngine;

public class RapidFireAbility : AbilityBase
{
    public override string AbilityName => "RapidFire";
    public override string Description => "Fires continuously in rapid succession but lowers attack power.";

    public override void Activate()
    {
        unit.RapidFireCount += 1;
        Debug.Log($"RapidFire ::: {unit.RapidFireCount}");
    }
 
    public override void StackAbility()
    {
        unit.RapidFireCount += 1;
        Debug.Log($"RapidFire ::: {unit.RapidFireCount}");
    }
}