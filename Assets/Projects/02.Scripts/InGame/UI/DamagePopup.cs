using System;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour, IUIElement
{
    private UIController uiController;
    private Camera mainCam;
    
    public Action OnUIImpact { get; set; }
    public Transform TargetTrs { get; set; }
    public TMP_Text DamageText { get; set; }

    private RectTransform rectTrs;

    private float lifeTime = 2f;
    private float timer;

    private Vector3 offset;
    
    private void Awake()
    {
        uiController = UIController.Instance;
        mainCam = Camera.main;
        rectTrs = GetComponent<RectTransform>();
        DamageText = GetComponent<TMP_Text>();
        offset = new Vector3(0f, 0.1f, 0f);
    }
    
    public void Show()
    {
        uiController.OnUIUpdate += UpdateUI;
        Vector2 pos =  mainCam.WorldToScreenPoint(TargetTrs.position + offset);
        rectTrs.position = pos;
        rectTrs.SetAsFirstSibling();
    }

    public void Hide()
    {
        timer = 0f;
        uiController.OnUIUpdate -= UpdateUI;
    }

    public void UpdateUI()
    {
        if (TargetTrs == null) return;
        
        rectTrs.position += new Vector3(0f, 20f * Time.deltaTime, 0f);
        
        timer += Time.deltaTime;
        if (timer >=  lifeTime)
        {
            OnUIImpact?.Invoke();
        }
    }
}
