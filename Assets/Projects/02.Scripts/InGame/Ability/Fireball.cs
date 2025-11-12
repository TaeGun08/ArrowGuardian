using UnityEngine;

public class Fireball : MonoBehaviour, IAbility
{
    public string AbilityName => "Fireball";
    public string Description => "Launches a blazing fireball that explodes on impact.";
    
    public void Activate(IDamageAble target)
    {
    }

    public void OnDuplicate(IDamageAble target)
    {
    }
}
