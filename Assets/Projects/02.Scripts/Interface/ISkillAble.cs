using UnityEngine;

public interface ISkillAble
{
   float Cooldown { get; }
   bool IsSkillActive { get; }
   
   void InitSkill();
   bool CanUseSkill();
   void UseSkill();
}
