using UnityEngine;

public interface IRunTimeStatus
{
    public int Health { get; set; }
    public int Damage { get; set; }
    public int Armor { get; set; }
}
