using UnityEngine;

public class LightningStrike : AbilityBase
{
    public override string AbilityName => "LightningStrike";
    public override string Description => "Summons a bolt of lightning that damages all nearby enemies.";

    public override void Activate()
    {
        Debug.Log("LightningStrike");
    }

    public override void StackAbility()
    {
    }
}
