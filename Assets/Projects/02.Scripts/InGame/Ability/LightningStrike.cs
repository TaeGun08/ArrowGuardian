using UnityEngine;

public class LightningStrike : IAbility
{
    public string AbilityName => "LightningStrike";
    public string Description => "Summons a bolt of lightning that damages all nearby enemies.";
    public IDamageAble Target { get; }

    public void Activate()
    {
        Debug.Log("LightningStrike");
    }

    public void StackAbility()
    {
    }
}
