using UnityEngine;

public class AttackSpeedBoost : MonoBehaviour, IAbility
{
    public string AbilityName => "AttackSpeedBoost";
    public string Description => "Increases attack speed for a short duration.";
    
    public void Activate(IDamageAble target)
    {
    }

    public void OnDuplicate(IDamageAble target)
    {
    }
}
