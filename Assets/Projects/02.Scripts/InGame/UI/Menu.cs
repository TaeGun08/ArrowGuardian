using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Menu Settings")]
    [SerializeField] private Transform contentTrs;
    [SerializeField] private AbilityUI abilityUIPrefab;
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject[] menuPanels;
    
    [SerializeField] private AbilityDataSO abilityDataSo;
    
    private readonly Dictionary<int, AbilityUI> abilityUIs = new Dictionary<int, AbilityUI>();

    private void Awake()
    {
        ButtonEvent();
    }

    private void ButtonEvent()
    {
        buttons[0].onClick.AddListener(() =>
        {
            menuPanels[0]?.SetActive(true);
            menuPanels[1]?.SetActive(false);
        });
        
        buttons[1].onClick.AddListener(() =>
        {
            menuPanels[0]?.SetActive(false);
            menuPanels[1]?.SetActive(true);
        });
        
        buttons[2].onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            menuPanels[0]?.SetActive(true);
            menuPanels[1]?.SetActive(false);
            GameManager.Instance.SetGameState(GameManager.GameState.Playing);
        });
    }

    private AbilityUI InstantiateAbilityUI()
    {
        return Instantiate(abilityUIPrefab, contentTrs);
    }
    
    public void SetAbilityContent(IAbility ability)
    {
        int id = ability.Id;
        
        if (abilityUIs.TryGetValue(id, out AbilityUI abilityUI))
        {
            abilityUI.SetStack(1);
            return;
        }

        AbilityUI ui = InstantiateAbilityUI();
        ui.SetName(ability.AbilityName);
        Debug.Log(ability.Id);
        ui.SetIcon(abilityDataSo.GetIconSprite(ability.Id));
        abilityUIs.Add(id, ui);
    }
}