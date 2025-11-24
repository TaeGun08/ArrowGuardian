using UnityEngine;

public class FreezeEffect : IStatusEffect
{
    public IDamageAble Sender { get; set; }
    public IDamageAble Target { get; set; }
    
    private float duration;
    private float timer;

    private Freeze freeze;
    
    public void Apply()
    {
        duration = 2f;
        Enemy enemy = Target as Enemy;
        if (enemy == null) return;
        enemy.IMovement.IsStop = true;
        
        freeze = GeneratorManager.Instance.EffectGenerator.CreateAndGetPool<Freeze>(1);
        freeze.Target = Target;
        freeze.Activate();
    }

    public void UpdateStatusEffect()
    {
        if (Target?.GameObject.activeInHierarchy == false)
        {
            Remove();
            return;
        }
        
        timer += Time.deltaTime;
        
        if (duration <= timer)
        {
            Remove();
        }
    }

    public void ChangeDuration(float sum)
    {
    }

    public void Refresh()
    {
        timer = 0f;
    }

    public void Remove()
    {
        Enemy enemy = Target as Enemy;
        if (enemy != null)
        {
            enemy.IMovement.IsStop = false;
        }
        
        Sender = null;
        Target = null;
        
        freeze.Deactivate();
        
        CombatManager.Instance.RemoveStatusEffect(this);
    }
}
