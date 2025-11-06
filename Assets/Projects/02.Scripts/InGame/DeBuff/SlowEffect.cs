using UnityEngine;

public class SlowEffect : IStatusEffect
{
    private float duration;
    private float timer;

    public IDamageAble Target { get; set; }

    public void Apply()
    {
        duration = 2f;

        var enemy = Target as Enemy;
        if (enemy) enemy.Speed *= 0.5f;
        Debug.Log($"{Target?.GameObject.name} ::: Slowed");
    }

    public void UpdateStatusEffect()
    {
        if (Target == null)
        {
            Remove();
            return;
        }

        timer += Time.deltaTime;
        
        if (duration > timer) return;
        var enemy = Target as Enemy;
        if (enemy) enemy.Speed = enemy.EnemyData.MoveSpeed;
        Remove();
    }

    public void ChangeDuration(float sum)
    {
        duration = sum;
    }

    public void Refresh()
    {
        timer = 0f;
    }

    public void Remove()
    {
        Debug.Log($"{Target?.GameObject.name} ::: Slow End");
        Target = null;
        CombatManager.Instance.RemoveStatusEffect(this);
    }
}