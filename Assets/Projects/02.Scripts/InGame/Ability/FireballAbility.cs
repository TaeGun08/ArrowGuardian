using System.Collections.Generic;
using UnityEngine;

public class FireballAbility : AbilityBase
{
    public override string AbilityName => "Fireball";
    public override string Description => "Launches a blazing fireball that explodes on impact.";
    
    private List<Fireball> fireballs = new List<Fireball>();
    
    public override void Activate()
    {
        Debug.Log("Fireball");
        fireballs.Add(skillPrefabSo.GetPrefab<Fireball>(0));
    }

    public override void StackAbility()
    {
        fireballs.Add(skillPrefabSo.GetPrefab<Fireball>(0));
    }
}
