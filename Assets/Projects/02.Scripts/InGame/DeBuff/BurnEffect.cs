using UnityEngine;

public class BurnEffect : IStatusEffect
{
    private float tickTimer;
    private float tickDelay;
    
    private float duration;
    private float timer;
    
    public IDamageAble Target { get; set; }
    
    public void Apply()
    {
        tickTimer = 0f;
        tickDelay = 0.5f;
        duration = 2f;
    }

    public void UpdateStatusEffect()
    {
        if (Target == null)
        {
            Remove();
            return;
        }
        
        tickTimer += Time.deltaTime;
        timer += Time.deltaTime;
        
        if (tickDelay <= tickTimer)
        {
            Target.TakeDamage(1);
            tickTimer = 0f;
        }

        if (duration <= timer)
        {
            Remove();
        }
    }

    public void ChangeDuration(float sum)
    {
        duration = sum;
    }

    public void Refresh()
    {
        timer = 0f;
    }

    public void Remove() 
    {
        CombatManager.Instance.RemoveStatusEffect(this);
    }
}