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
    [SerializeField] private GameObject soldOut;

    [Header("UI")]
    [SerializeField] private Sprite[] sprites;
    
    public void Initialize(StoreData data)
    {
        cardImage.sprite = GetSprite(data.Id);
        cardName.text = data.Name;
        cardPrice.text = $"{data.Price} Gold";
        ForSale(data.IsForSale);
    }

    private Sprite GetSprite(int id)
    {
        return sprites[id] == null ?  sprites[0] : sprites[id];
    }

    private void ForSale(bool forSale)
    {
        soldOut.SetActive(!forSale);
    }
}
