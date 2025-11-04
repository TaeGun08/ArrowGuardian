using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : SingletonBase<CombatManager>
{
    public readonly List<IStatusEffect> statusEffectList = new List<IStatusEffect>();
    private Action combatEvent;

    private void Update()
    {
        if (combatEvent != null)
        {
            combatEvent?.Invoke();
        }
    }

    private float CalculateFinalDamage(float baseDamage, ElementType attacker, ElementType defender)
    {
        float multiplier = GetElementMultiplier(attacker, defender);
        float finalDamage = baseDamage * multiplier;
        return Mathf.Round(finalDamage);
    }

    /// <summary>
    /// 속성별 대미지 계수 반환
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="defender"></param>
    /// <returns></returns>
    private float GetElementMultiplier(ElementType attacker, ElementType defender)
    {
        if (attacker == ElementType.None || defender == ElementType.None)
            return 1f;

        if ((attacker == ElementType.Light && defender == ElementType.Dark) ||
            (attacker == ElementType.Dark && defender == ElementType.Light))
            return 2.0f;

        switch (attacker)
        {
            case ElementType.Flame when defender == ElementType.Wind:
            case ElementType.Water when defender == ElementType.Flame:
            case ElementType.Earth when defender == ElementType.Water:
            case ElementType.Wind when defender == ElementType.Earth:
                return 1.5f;

            case ElementType.Flame when defender == ElementType.Water:
            case ElementType.Water when defender == ElementType.Earth:
            case ElementType.Earth when defender == ElementType.Wind:
            case ElementType.Wind when defender == ElementType.Flame:
                return 0.5f;

            default:
                return 1f;
        }
    }

    public void HandleDamage(IDamageAble target, float baseDamage, ElementType attackerType)
    {
        if (target == null) return;

        ElementType defenderType = target is IElementType targetElement ? targetElement.ElementType : ElementType.None;

        float finalDamage = CalculateFinalDamage(baseDamage, attackerType, defenderType);

        target.TakeDamage((int)finalDamage);
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
                Debug.Log($"HandleApplyStatusEffect ::: {statusEffectList.Count}");
                statusEffect.Apply();
                combatEvent += statusEffect.UpdateStatusEffect;
            }
            else
            {
                IStatusEffect existing = statusEffectList.Find(e =>
                    e.Target == statusEffect.Target &&
                    e.GetType() == statusEffect.GetType());
                
                Debug.Log($"HandleApplyStatusEffect REFRESH ::: {statusEffectList.Count}");
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
        Debug.Log("RemoveStatusEffect");
        
        if (effect == null) return;
        combatEvent -= effect.UpdateStatusEffect;
        statusEffectList.Remove(effect);
    }
    
    /// <summary>
    /// 버프 적용
    /// </summary>
    public void HandleBuff(IBuffable target, Buff buff)
    {
        if (target == null) return;
        target.ApplyBuff(buff);
    }

    /// <summary>
    /// 디버프 적용
    /// </summary>
    public void HandleDeBuff(IDeBuffable target, DeBuff debuff)
    {
        if (target == null) return;
        target.ApplyDeBuff(debuff);
    }
}