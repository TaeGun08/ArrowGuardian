public interface IAbility
{
    public string AbilityName { get; }
    public string Description { get; }

    public void Activate(IDamageAble target);
    public void OnDuplicate(IDamageAble target);
}
