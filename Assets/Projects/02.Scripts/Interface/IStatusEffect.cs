using UnityEngine;

public interface IStatusEffect
{
    public void Apply(Enemy target);
    public void ChangeDuration(float sum);
    public void Remove(Enemy target); 
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