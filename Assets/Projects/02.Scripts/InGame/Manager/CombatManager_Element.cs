using UnityEngine;

public partial class CombatManager : SingletonBase<CombatManager>
{
    private float CalculateFinalDamage(IDamageAble sender, IDamageAble target, ElementType elementType)
    {
        float multiplier = GetElementMultiplier(elementType, target.ElementType);
        float finalDamage = sender.runTimeStats.Damage * multiplier;
        return Mathf.Round(finalDamage);
    }
    
    public void HandleDamage(IDamageAble sender, IDamageAble target, float multiplier, ElementType elementType)
    {
        if (target == null) return;
        float finalDamage = CalculateFinalDamage(sender, target, elementType);

        int sumDamage = (int)(finalDamage * multiplier);
        Debug.Log($"Sender ::: {sender.ElementType}, Target ::: {target.ElementType}, HitDamage ::: {sumDamage}");
        
        target.TakeDamage(sumDamage);
        
        var damagePopup = generatorManager.UIGenerator.CreateAndGetPool<DamagePopup>(0);
        damagePopup.TargetTrs = target.Transform;
        damagePopup.DamageText.text = $"{sumDamage}";
        damagePopup.Sender = sender;
        damagePopup.ElementType = elementType;
        damagePopup.Show();
        
        damagePopup.OnUIImpact += () =>
        {
            damagePopup.Hide();
            generatorManager.UIGenerator.ReturnPool<DamagePopup>(0, damagePopup);
            damagePopup.OnUIImpact = null;
        };
    }
}
