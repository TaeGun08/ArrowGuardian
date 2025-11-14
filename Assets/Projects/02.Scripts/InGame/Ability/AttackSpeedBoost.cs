using UnityEngine;

public class AttackSpeedBoost : AbilityBase
{
    public override string AbilityName => "AttackSpeedBoost";
    public override string Description => "Increases attack speed for a short duration.";
    
    public override void Activate()
    {
        Debug.Log("AttackSpeedBoost");
    }

    public override void StackAbility()
    {
    }
}
