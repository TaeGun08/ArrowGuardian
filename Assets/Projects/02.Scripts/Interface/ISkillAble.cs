using UnityEngine;

public interface ISkillAble
{
    public float Cooldown { get; }
   
   public void InitSkill();
   public bool CanUseSkill();
   public void UseSkill();
   public void UpdateSkill();
}
