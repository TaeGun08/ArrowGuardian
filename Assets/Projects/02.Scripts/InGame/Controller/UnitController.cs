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

            WaitForSeconds rapidWait = new WaitForSeconds(0.15f);

            for (int i = 0; i < unit.RapidFireCount; i++)
            {
                FireMultiShot(targetEnemy);

                if (i < unit.RapidFireCount - 1)
                    yield return rapidWait;
            }
        }
    }
    
    private void FireMultiShot(Enemy targetEnemy)
    {
        CreateArrowForTarget(targetEnemy);

        int count = unit.MultiShotCount;
        if (count <= 0) return;

        float step = 5f;

        for (int i = 1; i < count; i++)
        {
            int index = (i + 1) / 2;
            int sign = (i % 2 == 1) ? -1 : 1;

            float offset = step * index * sign;

            CreateArrowWithOffset(targetEnemy, offset);
        }
    }

    
    private void CreateArrowForTarget(Enemy enemy)
    {
        Arrow arrow = CreateArrow(enemy.transform.position);
        if (enemy.TryGetComponent(out IDamageAble damageTarget))
        {
            arrow.SetTarget(damageTarget);
            arrow.SetSender(unit);
        }
    }

    private void CreateArrowWithOffset(Enemy enemy, float angleOffset)
    {
        Vector2 dir = (enemy.transform.position - transform.position).normalized;
        float baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        float finalAngle = baseAngle + angleOffset;
        Quaternion rotation = Quaternion.Euler(0, 0, finalAngle - 90f);

        Arrow arrow = CreateArrow(enemy.transform.position, rotation);

        if (enemy.TryGetComponent(out IDamageAble damageTarget))
        {
            arrow.SetTarget(damageTarget);
            arrow.SetSender(unit);
        }
    }
    
    private Arrow CreateArrow(Vector2 targetPos, Quaternion? rotationOverride = null)
    {
        Arrow arrow = generatorManager.ArrowGenerator
            .CreateAndGetPool<Arrow>((int)unit.UnitData.ElementType);

        Vector2 direction = (targetPos - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion defaultRot = Quaternion.Euler(0, 0, angle - 90f);

        arrow.transform.position = transform.position;
        arrow.transform.rotation = rotationOverride ?? defaultRot;

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
