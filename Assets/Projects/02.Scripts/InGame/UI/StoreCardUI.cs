using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreCardUI : MonoBehaviour
{
    private StoreData storeData;
    
    [Header("Store Card UI Settings")]
    [SerializeField] private Image cardImage;
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text cardPrice;

    public void Initialize(StoreData storeData)
    {
        // cardImage.sprite = context.Icon;
        // cardName.text = context.Name;
        // cardPrice.text = context.Price.ToString();
    }
}
