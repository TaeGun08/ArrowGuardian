using System;
using UnityEngine;

public class Explosion : EffectBase
{
    public override int Id => 0;

    private float timer;
    
    public override void Activate()
    {
        gameObject.SetActive(true);
        timer = 0f;
    }

    private void LateUpdate()
    {
        timer += Time.deltaTime;
        if (timer < 1f) return;
        timer = 0f;
        Deactivate();
    }

    public override void Deactivate()
    {
        effectGenerator.ReturnPool(0, this);
    }
}
