using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreCardUI : MonoBehaviour
{
    public class Context
    {
        public Sprite Icon { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
    
    [Header("Store Card UI Settings")]
    [SerializeField] private Image cardImage;
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text cardPrice;

    public void Initialize(Context context)
    {
        cardImage.sprite = context.Icon;
        cardName.text = context.Name;
        cardPrice.text = context.Price.ToString();
    }
}
