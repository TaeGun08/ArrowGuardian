using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySelect : MonoBehaviour
{
    private GameManager gameManager;
    
    private AbilityDraft abilityDraft;
    private IAbility ability;

    private Button button;
    
    [Header("Ability Settings")]
    [SerializeField] private TMP_Text abilityName;
    [SerializeField] private TMP_Text description;

    private void Awake()
    {
        button = GetComponent<Button>();
        
        button.onClick.AddListener(() =>
        {
            ChoiceAbility();
        });
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void ChoiceAbility()
    {
        Debug.Log(abilityDraft);
        abilityDraft.AddOrStackAbility(ability);
        abilityDraft.DraftLayout.SetActive(false);
        gameManager.SetGameState(GameManager.GameState.Playing);
    }

    public void SetAbilityDraft(AbilityDraft draft)
    {
        abilityDraft = draft;
    }

    public void SetAbility(IAbility ability) 
    {
        this.ability = ability;
        abilityName.text = ability.AbilityName;
        description.text = ability.Description;
    }
}
