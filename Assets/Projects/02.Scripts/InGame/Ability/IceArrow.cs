using UnityEngine;

public class IceArrow : IAbility
{
    public string AbilityName => "IceArrow";
    public string Description => "Fires a freezing arrow that slows enemies on hit.";
    public IDamageAble Target { get; set; }
    
    public void Activate()
    {
        Debug.Log("IceArrow");
    }

    public void StackAbility()
    {
    }
}
