using UnityEngine;

public class EnemyData
{
    public int Id { get; set; }
    public ElementType ElementType { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Damage { get; set; }
    public float MoveSpeed { get; set; } 
    public float AttackDelay { get; set; }
    public int Armor { get; set; }
    public int SkillDamage { get; set; }
    public float SkillCooldown { get; set; }
}
