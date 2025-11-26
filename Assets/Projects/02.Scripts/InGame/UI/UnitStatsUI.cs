using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitStatsUI : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private Slider expBar;
    [SerializeField] private TMP_Text healthText;

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

    public void SetHealthText(string text)
    {
        healthText.text = text;
    }
}
