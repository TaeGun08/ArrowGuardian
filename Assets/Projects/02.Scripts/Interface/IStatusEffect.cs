using UnityEngine;

public interface IStatusEffect
{
    public bool IsExpired { get; }
    
    public void Apply(Enemy target);
    public void UpdateEffect(Enemy target, float deltaTime);
    public void Remove(Enemy target); 
}

