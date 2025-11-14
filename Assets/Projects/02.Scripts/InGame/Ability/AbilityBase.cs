using UnityEngine;

public class AbilityBase : IAbility
{
    protected Unit unit;
    
    public virtual string AbilityName { get; }
    public virtual string Description { get; }
    public  IDamageAble Target { get; }

    public virtual void Init(Unit unit)
    {
        this.unit = unit;
    }
    
    public virtual void Activate()
    {
    }

    public virtual void StackAbility()
    {
    }
}
