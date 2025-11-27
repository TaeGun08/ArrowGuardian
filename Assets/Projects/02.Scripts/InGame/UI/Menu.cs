using System.Collections.Generic;
using UnityEngine;

public class Menu : SingletonBase<Menu>
{
    [SerializeField] private Transform contentTrs;
    [SerializeField] private GameObject abilityUIPrefab;
    
    private readonly Dictionary<int, IAbility> abilityUIs = new Dictionary<int, IAbility>();

    public void SetAbilityContent(IAbility ability)
    {
        int id = ability.Id;
        
        if (abilityUIs.TryGetValue(id, out IAbility _ability))
        {
            
            return;
        }

        abilityUIs.Add(id, ability);
        
    }
}