using UnityEngine;

public interface IStatusEffect
{
    public IDamageAble Sender { get; set; }
    public IDamageAble Target { get; set; }
    
    public void Apply();
    public void UpdateStatusEffect();
    public void ChangeDuration(float sum);
    public void Refresh();
    public void Remove(); 
}

public static class StatusEffectFactory
{
    public static IStatusEffect CreateStatusEffect<T>() where T : IStatusEffect, new()
    {
        return new T();
    }
}
