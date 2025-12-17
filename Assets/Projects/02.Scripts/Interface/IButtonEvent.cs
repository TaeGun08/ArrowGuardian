using System;
using UnityEngine;

public interface IButtonEvent
{
    public Action OnButtonEvent { get; set; }
    
    public void ButtonPressed();
}
