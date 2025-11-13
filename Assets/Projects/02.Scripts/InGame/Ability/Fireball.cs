using UnityEngine;

public class Fireball : IAbility
{
    public string AbilityName => "Fireball";
    public string Description => "Launches a blazing fireball that explodes on impact.";
    public IDamageAble Target { get; }
    
    public void Activate()
    {
        Debug.Log("Fireball");
    }

    public void StackAbility()
    {
    }
}
