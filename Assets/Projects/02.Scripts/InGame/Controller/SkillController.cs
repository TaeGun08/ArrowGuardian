using System;
using UnityEngine;

public class SkillController : SingletonBase<SkillController>
{
    public Action OnSkillUpdated { get; set; }

    private void Update()
    {
        OnSkillUpdated?.Invoke();
    }
}
