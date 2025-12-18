using UnityEngine;

public class StoreButton : ButtonEventBase
{
    [Header("Store Button")]
    [SerializeField] private GameObject storePanel;

    public override void ButtonPressed()
    {
        storePanel.SetActive(true);
    }
}