using System.Collections;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private Unit unit;

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

    private WaitForSeconds attackDelayWait;

    private void Awake()
    {
        unit = GetComponent<Unit>();
    }

    private void Start()
    {
        attackDelayWait = new WaitForSeconds(unit.UnitData.AttackDelay);
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

            if (distance > unit.UnitData.Range) continue;

            WaitForSeconds wait = new WaitForSeconds(0.15f);

            for (int i = 0; i < unit.UnitData.RapidFireCount; i++)
            {
                Vector2 direction = (targetEnemy.transform.position - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0, 0, angle - 90f);

                Arrow arrow = Instantiate(
                    unit.ArrowPrefabSo.GetArrow((int)unit.UnitData.ElementType),
                    transform.position,
                    rotation
                );

                if (targetEnemy.TryGetComponent(out IDamageAble damageTarget) == false) continue;
                arrow.SetTarget(damageTarget);
                arrow.SetSender(unit);

                if (i == unit.UnitData.RapidFireCount - 1) continue;
                    yield return wait;
            }
        }
    }

    public void TakeDamage(int damage)
    {
    }
}