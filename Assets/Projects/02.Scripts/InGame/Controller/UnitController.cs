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
        generatorManager = GeneratorManager.Instance;
        StartCoroutine(ShotArrowCoroutine());
    }

    private IEnumerator ShotArrowCoroutine()
    {
        while (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(unit.AttackDelay);

            Enemy targetEnemy = generatorManager.EnemyGenerator.GetClosetEnemy(transform.position);

            if (targetEnemy == null) continue;

            float distance = Vector2.Distance(transform.position, targetEnemy.transform.position);

            if (distance > unit.UnitData.Range) continue;

            WaitForSeconds wait = new WaitForSeconds(0.15f);

            for (int i = 0; i < unit.UnitData.RapidFireCount; i++)
            {
                Arrow arrow = CreateArrow(targetEnemy);

                if (targetEnemy.TryGetComponent(out IDamageAble damageTarget) == false) continue;
                arrow.SetTarget(damageTarget);
                arrow.SetSender(unit);
                
                if (i == unit.UnitData.RapidFireCount - 1) continue;
                    yield return wait;
            }
        }
    }

    private Arrow CreateArrow(Enemy enemy)
    {
        Vector2 direction = (enemy.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90f);

        Arrow arrow = generatorManager.ArrowGenerator.CreateAndGetPool<Arrow>((int)unit.UnitData.ElementType);
        arrow.transform.rotation = rotation;
        arrow.transform.position = transform.position;

        arrow.OnArrowImpact += () =>
        {
            generatorManager.ArrowGenerator.ReturnPool<Arrow>((int)unit.UnitData.ElementType, arrow);
            arrow.OnArrowImpact = null;
        };

        return arrow;
    }

    public void TakeDamage(int damage)
    {
    }
}