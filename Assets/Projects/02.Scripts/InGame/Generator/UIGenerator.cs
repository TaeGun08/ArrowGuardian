using System;
using System.Collections.Generic;
using UnityEngine;

public interface IUIElement
{
    public Action OnUIImpact { get; }
    public void Show();
    public void Hide();
    public void UpdateUI(); 
}

public class UIGenerator : GeneratorBase
{
    private void Awake()
    {
        prefabSoBase = Resources.Load<UIPrefabSO>("UIPrefabSO");
        parentTransform = ComponentExtensions.FindOrAddComponent<Canvas>().transform;
    }
}
