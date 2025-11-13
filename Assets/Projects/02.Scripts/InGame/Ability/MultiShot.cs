using UnityEngine;

public class MultiShot : IAbility
{
    public string AbilityName => "MultiShot";
    public string Description => "Fires multiple projectiles at once but slightly reduces attack power.";
    public IDamageAble Target { get; }

    public void Activate()
    {
        Debug.Log("MultiShot");
    }

    public void StackAbility()
    {
    }
}
