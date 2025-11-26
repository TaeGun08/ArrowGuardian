using UnityEngine;

public interface IMovement
{
    public bool IsStop { get; set; }
    public bool IsStunned { get; set; }
    public float Speed { get; set; }
}
