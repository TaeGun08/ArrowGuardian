using UnityEngine;

[CreateAssetMenu(fileName = "AbilityDataSO", menuName = "Scriptable Objects/AbilityDataSO")]
public class AbilityDataSO : ScriptableObject
{
    public IAbility GetAbility(int abilityIndex)
    {
        return abilityIndex switch
        {
            0 => new AttackBoost(),
            1 => new AttackSpeedBoost(),
            2 => new Fireball(),
            3 => new IceArrow(),
            4 => new LightningStrike(),
            5 => new MultiShot(),
            6 => new RapidFire(),
            _ => null
        };
    }
}
