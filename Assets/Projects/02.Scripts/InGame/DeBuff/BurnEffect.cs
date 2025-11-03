using UnityEngine;

public class BurnEffect : IStatusEffect
{
    private float elapsed;
    private float tickTimer;

    private float startAt;
    private float duration;
    public IDamageAble Target { get; set; }

//이거 지속성인지 단일성인지 나눠서 기능 구현해야 함    
    public void Apply()
    {
        float currentTime = Time.time - startAt;

        if (duration < currentTime)
        {
            
        }
    }
    
    public void ChangeDuration(float sum)
    {
        
    }

    public void Remove()
    {
        
    }
}