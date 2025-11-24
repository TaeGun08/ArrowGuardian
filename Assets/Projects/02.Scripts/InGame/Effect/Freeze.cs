using System;
using UnityEngine;

public class Freeze : EffectBase
{
    public override int Id => 1;
    
    public IDamageAble Target { get; set; }
    
    public override void Activate()
    {
        gameObject.SetActive(true);
        transform.position = Target.Transform.position;
    }

    public override void Deactivate()
    {
        Target = null;
        effectGenerator.ReturnPool(1, this);
    }
}
