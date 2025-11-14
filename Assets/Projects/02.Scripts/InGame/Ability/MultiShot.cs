using UnityEngine;

public class MultiShot : AbilityBase
{
    public override string AbilityName => "MultiShot";
    public override string Description => "Fires multiple projectiles at once but slightly reduces attack power.";

    public override void Activate()
    {
        Debug.Log("MultiShot");
    }

    public override void StackAbility()
    {
    }
}
