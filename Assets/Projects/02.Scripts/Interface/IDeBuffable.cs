using UnityEngine;

public interface IDeBuffable
{
    public IDamageAble Target { get; }     
    public void ApplyDebuff(DeBuff debuff);
}
