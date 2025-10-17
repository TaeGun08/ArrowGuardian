using UnityEngine;

[System.Serializable]
public class EnemyData
{
    [SerializeField] private ElementType elementType;
    public ElementType ElementType => elementType;
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Damage { get; set; }
    public float MoveSpeed { get; set; } 
    public float AttackDelay { get; set; }
    public int Armor { get; set; }
    public int SkillDamage { get; set; }
    public float SkillCooldown { get; set; }
}

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    [Header("Enemy Settings")]
    [SerializeField] private EnemyData[] enemyDatas;
    public EnemyData[] EnemyDatas => enemyDatas;

    public EnemyData GetEnemyData(int elementType)
    {
        foreach (var enemyData in EnemyDatas)
        {
            if ((int)enemyData.ElementType == elementType) return enemyData;
        }
        
        return enemyDatas[0];
    }
}
