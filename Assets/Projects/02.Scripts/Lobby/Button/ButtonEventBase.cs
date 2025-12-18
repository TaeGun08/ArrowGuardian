using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonEventBase : MonoBehaviour, IButtonEvent
{
    protected Button button;
    
    public Action OnButtonEvent { get; set; }

    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        
        button.onClick.AddListener(() => OnButtonEvent?.Invoke());
    }

    protected virtual void OnEnable()
    {
        OnButtonEvent += ButtonPressed;
    }

    protected virtual void OnDisable()
    {
        OnButtonEvent -= ButtonPressed;
    }

    public abstract void ButtonPressed();
}