using UnityEngine;

public class ArmorDebuff : IStatusEffect
{
    private float duration;
    private float timer;

    private int prevArmor;
    
    public IDamageAble Sender { get; set; }
    public IDamageAble Target { get; set; }

    public void Apply()
    {
        duration = 2f;
        int armor = Target.runTimeStats.Armor;
        prevArmor = armor;
        Target.runTimeStats.Armor = (int)(armor * 0.5f);
    }
    
    public void UpdateStatusEffect()
    {
        timer += Time.deltaTime;

        if (duration > timer) return;
        Target.runTimeStats.Armor = prevArmor;
        Remove();
    }

    public void SetSenderAndTarget(IDamageAble sender, IDamageAble target)
    {
        Sender = sender;
        Target = target;
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
