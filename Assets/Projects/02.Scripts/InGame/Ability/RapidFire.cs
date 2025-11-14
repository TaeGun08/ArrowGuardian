using UnityEngine;

public class RapidFire : AbilityBase
{
    public override string AbilityName => "RapidFire";
    public override string Description => "Fires continuously in rapid succession but lowers attack power.";

    public override void Activate()
    {
        Debug.Log("RapidFire");
    }

    public override void StackAbility()
    {
    }
}
