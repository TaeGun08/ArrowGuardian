using System;
using System.Collections;
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
    private ArrowSO arrowSO;
    private UnitData unitData = new UnitData();

    private void Awake()
    {
        unitData = UnitLoaderCSV.GetUnitByName(unitName.ToString());
        arrowSO = Resources.Load<ArrowSO>("ArrowSO");
    }

    private void Start()
    {
        StartCoroutine(ShotArrowCoroutine());
    }

    private IEnumerator ShotArrowCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(unitData.AttackDelay);
        while (gameObject.activeInHierarchy)
        {
            yield return wait;

            Collider2D enemy = Physics2D.OverlapCircle(transform.position, unitData.Range, LayerMask.GetMask("Enemy"));

            if (!enemy) continue;
            Vector2 direction = (enemy.transform.position - transform.position).normalized;
                
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle - 90f);

            Instantiate(arrowSO.GetArrow((int)unitData.ElementType), transform.position, rotation);
        }
    }
}