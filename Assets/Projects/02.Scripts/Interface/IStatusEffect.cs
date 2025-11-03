using UnityEngine;

public interface IStatusEffect
{
    public IDamageAble Target { get; set; }
    
    public void Apply();
    public void ChangeDuration(float sum);
    public void Remove(); 
}

public static class StatusEffectFactory
{
    public static IStatusEffect CreateStatusEffect<T>() where T : IStatusEffect, new()
    {
        return new T();
    }
}

public static class BuffDebuffFactory
{
    
}