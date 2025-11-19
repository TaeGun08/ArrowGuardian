using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Ability
{
    AttackBoost,
    AttackSpeedBoost,
    Fireball,
    IceArrow,
    LightningStrike,
    MultiShot,
    RapidFire,
}

public class AbilityDraft : MonoBehaviour
{
    private Dictionary<string, IAbility> abilities = new Dictionary<string, IAbility>();
    private AbilitySelect[] abilitySelects;

    [Header("DraftLayout")]
    [field: SerializeField] public GameObject DraftLayout { get; private set; }

    private AbilityDataSO abilityDataSo;

    private Action OnAbilityUpdated { get; set; }

    private void Awake()
    {
        abilitySelects = DraftLayout.GetComponentsInChildren<AbilitySelect>();

        foreach (var abilitySelect in abilitySelects)
        {
            abilitySelect.SetAbilityDraft(this);
        }

        abilityDataSo = Resources.Load<AbilityDataSO>("AbilityDataSO");
        
        for (int i = 0; i < abilitySelects.Length; i++)
        {
            abilitySelects[i].SetAbilityDraft(this);
        }
    }

    private void Start()
    {
        GameManager.Instance.DraftAction += () =>
        {
            DraftLayout.SetActive(true);

            List<IAbility> abilities = GetRandomAbilities();

            for (int i = 0; i < abilitySelects.Length; i++)
            {
                abilitySelects[i].SetAbility(abilities[i]);
            }
        };
    }

    private void Update()
    {
        OnAbilityUpdated?.Invoke();
    }

    public void AddOrStackAbility(IAbility ability)
    {
        if (!abilities.TryAdd(ability.AbilityName, ability))
        {
            abilities[ability.AbilityName].StackAbility();
            return;
        }
        
        abilities[ability.AbilityName].Activate();
        OnAbilityUpdated += abilities[ability.AbilityName].UpdateAbility;
    }
    
    public List<IAbility> GetRandomAbilities(int count = 3)
    {
        List<IAbility> result = new List<IAbility>();
        List<int> abilityIndex = new List<int>();

        while (result.Count < count)
        {
            int randomIndex = Random.Range(0, (int)Ability.RapidFire);

            if (abilityIndex.Contains(randomIndex)) continue;
            
            IAbility ability = abilityDataSo.GetAbility(randomIndex);
            AbilityBase abilityBase = ability as AbilityBase;
            abilityBase?.Init();
            result.Add(ability);
            abilityIndex.Add(randomIndex);
        }

        return result;
    }
}