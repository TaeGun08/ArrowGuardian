using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public enum UnitName
    {
        ApprenticeArcher,
        EmberArcher,
        TidalArcher,
        GaleArcher,
        StoneArcher,
        StormArcher,
        ShadowArcher,
        RadiantArcher,
    }
    
    [Header("Unit Settings")]
    [SerializeField] private UnitName unitName;
    
    private void Update()
    {
        UpdateShotArrow();
    }

    private void UpdateShotArrow()
    {
        
    }
}
