using UnityEngine;

[CreateAssetMenu(fileName = "StageDataSO", menuName = "Scriptable Objects/StageDataSO")]
public class StageDataSO : ScriptableObject
{
    [System.Serializable]
    public class StageData
    {
        public int Count { get; set; }
        public float Delay { get; set; }
        public ElementType ElementType { get; set; }
    }
    
    [Header("StageData Settigns")]
    [SerializeField] private StageData[] stageData;

    public StageData GetStageData()
    {
        
        return stageData[0];
    }
}
