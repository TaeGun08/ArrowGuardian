using UnityEngine;

public class Fireball : AbilityBase
{
    public override string AbilityName => "Fireball";
    public override string Description => "Launches a blazing fireball that explodes on impact.";
    
    public override void Activate()
    {
        Debug.Log("Fireball");
    }

    public override void StackAbility()
    {
    }
}
