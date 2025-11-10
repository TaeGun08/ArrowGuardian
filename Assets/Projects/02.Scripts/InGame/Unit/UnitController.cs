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

    private GeneratorManager generatorManager;

    [Header("Unit Settings")] [SerializeField]
    private UnitName unitName;

    private ArrowPrefabSO arrowPrefabSo;
    private UnitData unitData;

    public GameObject GameObject => gameObject;
    public Transform Transform => transform;
    public IRunTimeStatus IRunTimeStatus => this;
    public IMovement IMovement { get; }
    public ElementType ElementType { get; private set; }

    public int Health { get; set; }
    public int Damage { get; set; }
    public int Armor { get; set; }

    private WaitForSeconds attackDelayWait;

    private void Awake()
    {
        unitData = UnitLoaderCSV.GetUnitByName(unitName.ToString());

        ElementType = unitData.ElementType;
        Damage = unitData.Damage;

        arrowPrefabSo = Resources.Load<ArrowPrefabSO>("ArrowPrefabSO");
        attackDelayWait = new WaitForSeconds(unitData.AttackDelay);
    }

    private void Start()
    {
        generatorManager = GeneratorManager.Instance;
        StartCoroutine(ShotArrowCoroutine());
    }

    private IEnumerator ShotArrowCoroutine()
    {
        while (gameObject.activeInHierarchy)
        {
            yield return attackDelayWait;

            Enemy targetEnemy = generatorManager.EnemyGenerator.GetClosetEnemy(transform.position);

            if (targetEnemy == null) continue;

            float distance = Vector2.Distance(transform.position, targetEnemy.transform.position);

            if (distance > unitData.Range) continue;

            Vector2 direction = (targetEnemy.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle - 90f);

            Arrow arrow = Instantiate(
                arrowPrefabSo.GetArrow((int)unitData.ElementType),
                transform.position,
                rotation
            );

            if (targetEnemy.TryGetComponent(out IDamageAble damageTarget) == false) continue;
            arrow.SetTarget(damageTarget);
            arrow.SetSender(this);
        }
    }

    private void OnDrawGizmos()
    {
        if (unitData == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, unitData.Range);
    }

    public void TakeDamage(int damage)
    {
    }
}