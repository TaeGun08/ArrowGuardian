using UnityEngine;

public interface IAbility
{
    public string AbilityName { get; }
    public string Description { get; }
    public Sprite Icon { get; }
    public IDamageAble Target { get; }
    
    public void Activate();
    public void UpdateAbility();
    public void StackAbility();
}
