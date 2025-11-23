using UnityEngine;

public interface IEffectable
{
    public int Id { get; }
    
    public void Activate();
    public void Deactivate();
}
