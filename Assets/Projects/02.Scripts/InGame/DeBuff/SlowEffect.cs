using UnityEngine;

public class SlowEffect : IStatusEffect
{
    private float duration;
    private float timer;

    private float prevMoveSpeed;
    
    public IDamageAble Sender { get; set; }
    public IDamageAble Target { get; set; }

    public void Apply()
    {
        duration = 2f;

        prevMoveSpeed = Target.IMovement.Speed;
        Target.IMovement.Speed *= 0.5f;
        Debug.Log($"{Target?.GameObject.name} ::: Slowed");
    }
    
    public void UpdateStatusEffect()
    {
        if (Target?.GameObject == null)
        {
            Remove();
            return;
        }
        
        timer += Time.deltaTime;

        if (duration > timer) return;
        Target.IMovement.Speed = prevMoveSpeed;
        Remove();
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
        Sender = null;
        Target = null;
        CombatManager.Instance.RemoveStatusEffect(this);
    }
}