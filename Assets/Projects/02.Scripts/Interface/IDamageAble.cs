using UnityEngine;

public interface IDamageAble
{
    public ElementType ElementType { get; }
    public GameObject GameObject { get; }
    public Transform Transform { get; }
    public IRunTimeStatus IRunTimeStatus { get; }
    public IMovement IMovement { get; }
    
    public void TakeDamage(int damage);
}
