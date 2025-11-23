using System;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour, ISkillAble
{
    protected CombatManager combatManager;
    protected SkillController skillController;
    protected EnemyGenerator enemyGenerator;
    protected EffectGenerator effectGenerator;
    protected Camera mainCamera;

    public Action OnSkillImpact { get; set;  }
    public float Cooldown { get; }
    
    public virtual void InitSkill()
    {
        combatManager = CombatManager.Instance;
        skillController = SkillController.Instance;
        enemyGenerator = GeneratorManager.Instance.EnemyGenerator;
        effectGenerator = GeneratorManager.Instance.EffectGenerator;
        mainCamera = Camera.main;
        skillController.OnSkillUpdated += UpdateSkill;
    }

    public abstract void UseSkill();

    public abstract void UpdateSkill();
}
