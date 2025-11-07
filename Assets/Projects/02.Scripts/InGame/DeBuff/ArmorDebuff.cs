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

        int armor = Target.IRunTimeStatus.Armor;
        prevArmor = armor;
        Target.IRunTimeStatus.Armor = (int)(armor * 0.5f);
        Debug.Log($"{Target?.GameObject.name} ::: Slowed");
    }
    
    public void UpdateStatusEffect()
    {
        timer += Time.deltaTime;

        if (duration > timer) return;
        Target.IRunTimeStatus.Armor = prevArmor;
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
