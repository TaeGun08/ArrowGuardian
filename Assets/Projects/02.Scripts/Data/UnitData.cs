using System;

[Serializable]
public class UnitData
{
    public string Name { get; set; }
    public ElementType ElementType { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Damage { get; set; }
    public int Range { get; set; }
    public float AttackDelay { get; set; }
    public int Armor { get; set; }
    public float ProjectileSpeed { get; set; }
    public float CritChance { get; set; }
    public float CritDamage { get; set; }
    public int SkillDamage { get; set; }
    public float SkillCooldown { get; set; }
    public float SplashRadius { get; set; }
    public bool AppliesDebuff { get; set; }
}