using UnityEngine;

public class AttackSpeedBoostAbility : AbilityBase
{
    public override int Id => 1;
    public override string AbilityName => "AttackSpeedBoost";
    public override string Description => "Increases attack speed for a short duration.";

    private float diffDelay;

    public override void Activate()
    {
        diffDelay = unit.UnitData.AttackDelay * 0.05f;
        unit.AttackDelay -= diffDelay;
        Debug.Log($"AttackSpeedBoost ::: {unit.AttackDelay}");
    }


    public override void StackAbility()
    {
        unit.AttackDelay -= diffDelay;
        Debug.Log($"AttackSpeedBoost ::: {unit.AttackDelay}");
    }
}