using UnityEngine;

public class AbilityBase : IAbility
{
    protected Unit unit;
    protected SkillGenerator skillGenerator;
    protected GeneratorManager generatorManager;

    public virtual int Id { get; set; }
    public virtual string AbilityName { get; }
    public virtual string Description { get; }
    
    public  IDamageAble Target { get; }
    

    public virtual void Init()
    {
        unit = Unit.Instance;
        generatorManager = GeneratorManager.Instance;
        skillGenerator = generatorManager.SkillGenerator;
    }
    
    public virtual void Activate()
    {
    }

    public virtual void UpdateAbility()
    {
    }
    
    public virtual void StackAbility()
    {
    }
}
