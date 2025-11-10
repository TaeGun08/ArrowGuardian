using UnityEngine;

[CreateAssetMenu(fileName = "WaveDataSO", menuName = "Scriptable Objects/WaveDataSO")]
public class WaveDataSO : ScriptableObject
{
    [System.Serializable]
    public class WaveData
    {
        public int Count;

        public float Delay;

        public ElementType GetElementType(int index)
        {
            return (ElementType)index;
        }
    }

    [Header("Wave Settings")]
    [SerializeField] private WaveData[] waves;

    public WaveData GetWaveData(int index)
    {
        if (waves == null || waves.Length == 0) return null;

        if (index < 0 || index >= waves.Length)
            return waves[waves.Length - 1];

        return waves[index];
    }

    public int GetWaveCount() => waves?.Length ?? 0;
}