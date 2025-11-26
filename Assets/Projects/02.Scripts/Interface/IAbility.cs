using UnityEngine;

public interface IAbility
{
    public int Id { get; }
    public string AbilityName { get; }
    public string Description { get; }
    public IDamageAble Target { get; }
    
    public void Activate();
    public void UpdateAbility();
    public void StackAbility();
}
