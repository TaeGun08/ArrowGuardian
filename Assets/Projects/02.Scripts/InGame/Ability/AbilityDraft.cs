using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AbilityDraft : MonoBehaviour
{
    private List<IAbility> abilityList = new List<IAbility>();
    private AbilitySelect[] abilitySelects;

    [Header("DraftLayout")] 
    [field: SerializeField] public GameObject DraftLayout { get; private set; }

    private void Awake()
    {
        abilitySelects = GetComponentsInChildren<AbilitySelect>();

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

            for (int i = 0; i < abilities.Count; i++)
            {
                abilitySelects[i].SetAbility(abilities[i]);
            }
        };
    }

    public void AddListAbility(IAbility ability)
    {
        abilityList.Add(ability);
    }
    
    public List<IAbility> GetRandomAbilities(int count = 3)
    {
        List<IAbility> result = new List<IAbility>();
        List<IAbility> pool = new List<IAbility>(abilityList);

        for (int i = 0; i < count && pool.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, pool.Count);
            result.Add(pool[randomIndex]);
            pool.RemoveAt(randomIndex);
        }

        return result;
    }
}
 