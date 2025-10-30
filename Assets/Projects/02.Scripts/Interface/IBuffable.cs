using System.Collections.Generic;
using UnityEngine;

public interface IBuffable
{
    public IDamageAble Target { get; }
    
    public void ApplyBuff(Buff buff);
}
