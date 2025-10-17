using System;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    private const int MAX_EVENT_PROCESS_COUNT = 10;
    
    public class Callback
    {
        public Action<CombatSystem> OnCombatEvent;
    }
    
    private Action combatEvent;

    private void Update()
    {
        
    }

    public void GetCombatEvent(Action combatEvent)
    {
        
    }
}
