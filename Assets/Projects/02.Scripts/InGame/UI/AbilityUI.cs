using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [Header("Ability UI")]
    [SerializeField] private TMP_Text abilityName;
    [SerializeField] private Image abilityImage;
    [SerializeField] private TMP_Text abilityStack;
    private int stackCount = 1;
    
    public void SetName(string name)
    {
        abilityName.text = name;
    }
    
    public void SetIcon(Sprite icon)
    {
        abilityImage.sprite = icon;
    }
    
    public void SetStack(int stack)
    {
        stackCount += stack;
        abilityStack.text = $"Stack: {stackCount}";
    }
}
