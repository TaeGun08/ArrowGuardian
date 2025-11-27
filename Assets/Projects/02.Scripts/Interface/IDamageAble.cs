using UnityEngine;

public interface IDamageAble
{
    public ElementType ElementType { get; }
    public GameObject GameObject { get; }
    public Transform Transform { get; }
    public IRunTimeStats runTimeStats { get; }
    public IMovement IMovement { get; }
    
    public int TakeDamage(int damage);
}
