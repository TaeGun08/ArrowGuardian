using System;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour, ISkillAble
{
    protected SkillController skillController;
    protected EnemyGenerator enemyGenerator;
    protected Camera mainCamera;

    public Action OnSkillImpact { get; set;  }
    public float Cooldown { get; }
    
    public virtual void InitSkill()
    {
        skillController = SkillController.Instance;
        enemyGenerator = GeneratorManager.Instance.EnemyGenerator;
        mainCamera = Camera.main;
        skillController.OnSkillUpdated += UpdateSkill;
    }

    public abstract void UseSkill();

    public abstract void UpdateSkill();
}
