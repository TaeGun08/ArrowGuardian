 using UnityEngine;

public class AttackBoost : IAbility
{
    public string AbilityName => "AttackBoost";
    public string Description => "Increases attack power for a short duration.";
    public IDamageAble Target { get; }
    
    public void Activate()
    {
        Debug.Log("AttackBoost");
    }

    public void StackAbility()
    {
    }
}
