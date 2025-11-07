using System;
using System.Collections;
using UnityEngine;

public class UnitController : MonoBehaviour, IDamageAble, IRunTimeStatus
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
    private ArrowPrefabSO arrowPrefabSo;
    private UnitData unitData = new UnitData();
    
    public GameObject GameObject => gameObject;
    public Transform Transform => transform;
    public IRunTimeStatus IRunTimeStatus => this;
    public IMovement IMovement { get; }
    public ElementType ElementType { get; set; }

    public int Health { get; set; }
    public int Damage { get; set; }
    public int Armor { get; set; }
    
    private void Awake()
    {
        unitData = UnitLoaderCSV.GetUnitByName(unitName.ToString());
        
        ElementType = unitData.ElementType;
        Damage = unitData.Damage;
        
        arrowPrefabSo = Resources.Load<ArrowPrefabSO>("ArrowPrefabSO");
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

            Arrow arrow = Instantiate(arrowPrefabSo.GetArrow((int)unitData.ElementType), transform.position, rotation);
            arrow.SetTarget(enemy.GetComponent<IDamageAble>());
            arrow.SetSender(this);
        }
    }
    
    public void TakeDamage(int damage)
    {
    }
}