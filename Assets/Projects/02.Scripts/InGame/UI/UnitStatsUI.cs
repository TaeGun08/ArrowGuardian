using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitStatsUI : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private Slider expBar;

    private void Start()
    {
        GameManager.Instance.UnitStatsUI = this;
    }
    
    public void SetWaveText(string text)
    {
        waveText.text = text;
    }

    public void SetExpBar(float value, float total)
    {
        expBar.value = value / total;
    }
}
