using UnityEngine;

public abstract class EffectBase : MonoBehaviour, IEffectable
{
    protected EffectGenerator effectGenerator;
    
    public abstract int Id { get; }

    protected virtual void Awake()
    {
        effectGenerator = GeneratorManager.Instance.EffectGenerator;
    }
    
    public abstract void Activate();
    public abstract void Deactivate();
}
