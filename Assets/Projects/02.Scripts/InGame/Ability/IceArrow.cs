using UnityEngine;

public class IceArrow : AbilityBase
{
    public override string AbilityName => "IceArrow";
    public override string Description => "Fires a freezing arrow that slows enemies on hit.";
    
    public override void Activate()
    {
        Debug.Log("IceArrow");
    }

    public override void StackAbility()
    {
    }
}
