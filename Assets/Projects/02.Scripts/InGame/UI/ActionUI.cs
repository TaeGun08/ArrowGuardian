using System;
using UnityEngine;

public class ActionUI : MonoBehaviour, IUIElement
{
    public Action OnUIImpact { get; set; }
    public IDamageAble Sender { get; set; }
    public IDamageAble Target { get; set; }

    public virtual void Show()
    {
    }

    public virtual void Hide()
    {
    }

    public virtual void UpdateUI()
    {
    }
}
