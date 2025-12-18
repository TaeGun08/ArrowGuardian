using UnityEngine;

public class PetsButton : ButtonEventBase
{
    [Header("Pets Button")]
    [SerializeField] private GameObject petsPanel;
    
    public override void ButtonPressed()
    {
        petsPanel.SetActive(true);
    }
}
