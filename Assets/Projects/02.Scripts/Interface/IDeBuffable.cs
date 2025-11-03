using UnityEngine;

public interface IDeBuffable
{
    public IDamageAble Target { get; }     
    public void ApplyDeBuff(DeBuff debuff);
}
