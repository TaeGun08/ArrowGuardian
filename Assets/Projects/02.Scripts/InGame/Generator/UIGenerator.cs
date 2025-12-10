using System;
using System.Collections.Generic;
using UnityEngine;

public interface IUIElement
{
    public Action OnUIImpact { get; }

    public IDamageAble Sender { get; }
    public IDamageAble Target { get; }

    public void Show();
    public void Hide();
    public void UpdateUI(); 
}

public sealed class UIGenerator : GeneratorBase
{
    private void Awake()
    {
        prefabSoBase = Resources.Load<UIPrefabSO>("UIPrefabSO");
        parentTransform = ComponentExtensions.FindOrAddComponent<Canvas>().transform;
    }
}
