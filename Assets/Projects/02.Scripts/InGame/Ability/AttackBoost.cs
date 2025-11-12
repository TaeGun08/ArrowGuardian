using UnityEngine;

public class AttackBoost : MonoBehaviour, IAbility
{
    public string AbilityName => "AttackBoost";
    public string Description => "Increases attack power for a short duration.";
    
    public void Activate(IDamageAble target)
    {
    }

    public void OnDuplicate(IDamageAble target)
    {
    }
}
