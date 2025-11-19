using UnityEngine;

[CreateAssetMenu(fileName = "AbilityDataSO", menuName = "Scriptable Objects/AbilityDataSO")]
public class AbilityDataSO : ScriptableObject
{
    public IAbility GetAbility(int abilityIndex)
    {
        return abilityIndex switch
        {
            0 => new AttackBoostAbility(),
            1 => new AttackSpeedBoostAbility(),
            2 => new FireballAbility(),
            3 => new IceArrowAbility(),
            4 => new LightningStrikeAbility(),
            5 => new MultiShotAbility(),
            6 => new RapidFireAbility(),
            _ => null
        };
    }
}
