using UnityEngine;

public class BurnEffect : IStatusEffect
{
    private float tickInterval;
    private float elapsed;
    private float tickTimer;
    private float damagePerTick;

    private float startAt;
    private float duration;
    
    public bool IsExpired => elapsed >= duration;

    public BurnEffect(float duration, float tickInterval, float damagePerTick)
    {
        this.duration = duration;
        this.tickInterval = tickInterval;
        this.damagePerTick = damagePerTick;
    }

    public void ChangeDuration(float sum)
    {
        
    }

    public void CheckLife(float currentTime) //currentTime = Time.time
    {
        
    }
    
    public void Apply(Enemy target)
    {
        float currentTime = Time.time - startAt;

        if (duration < currentTime) // 버프 종료
        {
            
        }
        


    }

    public void UpdateEffect(Enemy target, float deltaTime)
    {
        elapsed += deltaTime;
        tickTimer += deltaTime;

        if (tickTimer < tickInterval) return;
        tickTimer = 0f;
        target?.TakeDamage(1);
    }

    public void Remove(Enemy target)
    {
    }
}