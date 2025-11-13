using UnityEngine;

public class RapidFire : IAbility
{
    public string AbilityName => "RapidFire";
    public string Description => "Fires continuously in rapid succession but lowers attack power.";
    public IDamageAble Target { get; }

    public void Activate()
    {
        Debug.Log("RapidFire");
    }

    public void StackAbility()
    {
    }
}
