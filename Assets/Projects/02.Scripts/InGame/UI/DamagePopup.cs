using System;
using TMPro;
using UnityEngine;

public class DamagePopup : ActionUI
{
    private UIController uiController;
    private Camera mainCam;
    
    public Transform TargetTrs { get; set; }
    public TMP_Text DamageText { get; set; }
    public Vector3 Offset { get; set; }

    private RectTransform rectTrs;

    private float lifeTime = 2f;
    private float timer;
    
    public ElementType ElementType { get; set; }
    
    private void Awake()
    {
        uiController = UIController.Instance;
        mainCam = Camera.main;
        rectTrs = GetComponent<RectTransform>();
        DamageText = GetComponent<TMP_Text>();
    }
    
    public override void Show()
    {
        uiController.OnUIUpdate += UpdateUI;
        Vector2 pos =  mainCam.WorldToScreenPoint(TargetTrs.position + Offset);
        rectTrs.position = pos;
        DamageText.color = ChangeTextColor();
        rectTrs.SetAsFirstSibling();
    }

    public override void Hide()
    {
        timer = 0f;
        uiController.OnUIUpdate -= UpdateUI;
        Sender = null;
        Target = null;
    }

    public override void UpdateUI()
    {
        if (TargetTrs == null) return;
        
        rectTrs.position += new Vector3(0f, 20f * Time.deltaTime, 0f);
        
        timer += Time.deltaTime;
        if (timer >=  lifeTime)
        {
            OnUIImpact?.Invoke();
        }
    }

    private Color ChangeTextColor()
    {
        switch (ElementType)
        {
            case ElementType.Flame:
                return Color.red;
            case ElementType.Water:
                return Color.cyan;
            case ElementType.Wind:
                return Color.green;
            case ElementType.Earth:
                return new Color(115f, 63f, 43f);
            case ElementType.Lightning:
                return Color.yellow;
            case ElementType.Dark:
                return Color.clear;
            case ElementType.Light:
                return new Color(255f, 252f, 162f);
        }
        
        return Color.white;
    }
}
