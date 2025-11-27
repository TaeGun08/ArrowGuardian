using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [Header("Ability UI")]
    [SerializeField] private TMP_Text abilityName;
    [SerializeField] private Image abilityImage;
    [SerializeField] private TMP_Text abilityStack;
    
    public void SetName(string name)
    {
        abilityName.text = name;
    }
    
    public void SetIcon(Sprite icon)
    {
        abilityImage.sprite = icon;
    }
    
    public void SetStack(string stack)
    {
        abilityStack.text = stack;
    }
}
