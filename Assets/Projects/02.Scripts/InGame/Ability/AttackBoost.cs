using UnityEngine;

public class AttackBoost : MonoBehaviour, IAbility
{
    public string AbilityName => "AttackBoost";
    public string Description { get; }
    
    public void Activate(IDamageAble target)
    {
        
    }
}
