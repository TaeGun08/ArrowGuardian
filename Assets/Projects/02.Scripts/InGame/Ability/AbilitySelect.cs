using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySelect : MonoBehaviour
{
    private GameManager gameManager;
    private Menu menu;
    
    private AbilityDraft abilityDraft;
    private IAbility ability;

    private Button button;
    
    private AbilityDataSO abilityDataSo;
    
    [Header("Ability Settings")]
    [SerializeField] private TMP_Text abilityName;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Image icon;
    
    private void Awake()
    {
        menu = Menu.Instance;
        
        button = GetComponent<Button>();
     
        abilityDataSo = Resources.Load<AbilityDataSO>("AbilityDataSO");
        
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
        icon.sprite = abilityDataSo.GetIconSprite(ability.Id);
    }
}
