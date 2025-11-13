using UnityEngine;

public class AttackSpeedBoost : IAbility
{
    public string AbilityName => "AttackSpeedBoost";
    public string Description => "Increases attack speed for a short duration.";
    public IDamageAble Target { get; }
    
    public void Activate()
    {
        Debug.Log("AttackSpeedBoost");
    }

    public void StackAbility()
    {
    }
}
