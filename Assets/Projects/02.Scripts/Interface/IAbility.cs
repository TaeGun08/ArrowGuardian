public interface IAbility
{
    public string AbilityName { get; }
    public string Description { get; }
    public IDamageAble Target { get; }
    
    public void Activate();
    public void StackAbility();
}
