using UnityEngine;

public class BurnEffect : IStatusEffect
{
    private float elapsed;
    private float tickTimer;

    private float startAt;
    private float duration;
    
    public void CheckLife(float currentTime)
    {
        
    }
    
    public void Apply(Enemy target)
    {
        float currentTime = Time.time - startAt;

        if (duration < currentTime)
        {
            
        }
    }
    
    public void ChangeDuration(float sum)
    {
        
    }

    public void Remove(Enemy target)
    {
        
    }
}