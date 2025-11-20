using System;
using UnityEngine;

public interface ISkillAble
{ 
   public Action OnSkillImpact { get; } 
   
   public float Cooldown { get; }
   
   public void InitSkill();
   public void UseSkill();
   public void UpdateSkill();
}
