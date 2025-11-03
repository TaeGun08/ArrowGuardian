using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatManager : SingletonBase<CombatManager>
{
    private float CalculateFinalDamage(float baseDamage, ElementType attacker, ElementType defender)
    {
        float multiplier = GetElementMultiplier(attacker, defender);
        float finalDamage = baseDamage * multiplier;
        return Mathf.Round(finalDamage);
    }
    
    /// <summary>
    /// 속성별 
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

    public void HandleApplyStatusEffect(List<IStatusEffect> statusEffects)
    {
        for (int i = 0; i < statusEffects.Count; i++)
        {
            var statusEffect = statusEffects[i];
            
            switch (statusEffect)
            {
                case BurnEffect burnEffect:
                    burnEffect.Apply();
                    break;
                case SlowEffect slowEffect:
                    slowEffect.Apply();
                    break;
                default:
                    break;
            }
        }
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
