using System;
using UnityEngine;

public class UIController : SingletonBase<UIController>
{
    public Action OnUIUpdate { get; set; }

    private void LateUpdate()
    {
        OnUIUpdate?.Invoke();
    }
}
