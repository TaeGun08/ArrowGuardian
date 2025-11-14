using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : SingletonBase<CombatManager>
{ 
    public readonly List<IStatusEffect> statusEffectList = new List<IStatusEffect>();
    private Action statusEffectEvent;

    private void Update()
    {
        statusEffectEvent?.Invoke();
    }

    private float CalculateFinalDamage(IDamageAble sender, IDamageAble target)
    {
        float multiplier = GetElementMultiplier(sender.ElementType, target.ElementType);
        float finalDamage = sender.runTimeStats.Damage * multiplier;
        return Mathf.Round(finalDamage);
    }

    /// <summary>
    /// 속성별 대미지 계수 반환
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    private float GetElementMultiplier(ElementType sender, ElementType target)
    {
        if (sender == ElementType.None || target == ElementType.None)
            return 1f;

        if ((sender == ElementType.Light && target == ElementType.Dark) ||
            (sender == ElementType.Dark && target == ElementType.Light))
            return 2.0f;

        switch (sender)
        {
            case ElementType.Flame when target == ElementType.Wind:
            case ElementType.Water when target == ElementType.Flame:
            case ElementType.Earth when target == ElementType.Water:
            case ElementType.Wind when target == ElementType.Earth:
                return 1.5f;

            case ElementType.Flame when target == ElementType.Water:
            case ElementType.Water when target == ElementType.Earth:
            case ElementType.Earth when target == ElementType.Wind:
            case ElementType.Wind when target == ElementType.Flame:
                return 0.5f;

            default:
                return 1f;
        }
    }

    public void HandleDamage(IDamageAble sender, IDamageAble target, float multiplier)
    {
        if (target == null) return;
        float finalDamage = CalculateFinalDamage(sender, target);
 
        Debug.Log($"Sender ::: {sender.ElementType}, Target ::: {target.ElementType}, HitDamage ::: {(int)(finalDamage * multiplier)}");
        target.TakeDamage((int)(finalDamage * multiplier));
    }

    public void HandleApplyStatusEffect(List<IStatusEffect> newStatusEffects)
    {
        foreach (var statusEffect in newStatusEffects)
        {
            bool exists = statusEffectList.Exists(e =>
                e.Target == statusEffect.Target &&
                e.GetType() == statusEffect.GetType());
            
            if (exists == false)
            {
                statusEffectList.Add(statusEffect);
                statusEffect.Apply();
                statusEffectEvent += statusEffect.UpdateStatusEffect;
            }
            else
            {
                IStatusEffect existing = statusEffectList.Find(e =>
                    e.Target == statusEffect.Target &&
                    e.GetType() == statusEffect.GetType());
                
                existing?.Refresh();
            }
        }
    }

    /// <summary>
    /// 효과 제거
    /// </summary>
    /// <param name="effect"></param>
    public void RemoveStatusEffect(IStatusEffect effect)
    {
        if (effect == null) return;
        statusEffectEvent -= effect.UpdateStatusEffect;
        statusEffectList.Remove(effect);
    }
}