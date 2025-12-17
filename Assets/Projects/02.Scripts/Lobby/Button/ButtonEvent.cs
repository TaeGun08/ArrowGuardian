using System;
using UnityEngine;
using UnityEngine.UI;

public abstract partial class ButtonEvent : MonoBehaviour, IButtonEvent
{
    protected Button button;
    
    public Action OnButtonEvent { get; set; }

    protected void Awake()
    {
        button = GetComponent<Button>();
    }

    protected void OnEnable()
    {
        OnButtonEvent += ButtonPressed;
    }

    protected void OnDisable()
    {
        OnButtonEvent -= ButtonPressed;
    }

    public abstract void ButtonPressed();
}
