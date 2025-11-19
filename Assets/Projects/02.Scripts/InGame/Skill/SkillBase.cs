using UnityEngine;

public abstract class SkillBase : MonoBehaviour, ISkillAble
{
    protected SkillController skillController;
    protected Camera mainCamera;
    
    public float Cooldown { get; }
    
    public virtual void InitSkill()
    {
        skillController = SkillController.Instance;
        mainCamera = Camera.main;
        skillController.OnSkillUpdated += UpdateSkill;
    }

    public abstract bool CanUseSkill();

    public abstract void UseSkill();

    public abstract void UpdateSkill();
}
