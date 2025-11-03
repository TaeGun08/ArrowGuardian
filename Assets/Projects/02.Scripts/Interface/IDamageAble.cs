using UnityEngine;

public interface IDamageAble
{
    public GameObject GameObject { get; }
    public Transform Transform { get; }
    
    public void TakeDamage(int damage);
}
