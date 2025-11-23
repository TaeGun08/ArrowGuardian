using UnityEngine;

public abstract class EffectBase : MonoBehaviour, IEffectable
{
    public abstract int Id { get; }
    
    public abstract void Activate();
    public abstract void Deactivate();
}
