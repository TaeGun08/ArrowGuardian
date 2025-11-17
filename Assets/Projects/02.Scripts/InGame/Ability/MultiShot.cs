using UnityEngine;

public class MultiShot : AbilityBase
{
    public override string AbilityName => "MultiShot";
    public override string Description => "Fires multiple projectiles at once but slightly reduces attack power.";
    
    public override void Activate()
    {
        unit.MultiShotCount += 1;
        Debug.Log($"MultiShot ::: {unit.MultiShotCount}");
    }

    public override void StackAbility()
    {
        unit.MultiShotCount += 1;
        Debug.Log($"MultiShot ::: {unit.MultiShotCount}");
    }
}
