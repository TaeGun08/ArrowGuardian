using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : ActionUI
{
    private const float LIFETIME = 2f;
    private const float MOVE_SPEED = 20f;
    
    private UIController uiController;
    private Camera mainCam;
    private RectTransform rectTrs;

    public Transform TargetTrs { get; set; }
    public TMP_Text DamageText { get; private set; }
    public Vector3 Offset { get; set; }
    public ElementType ElementType { get; set; }

    private float timer;

    private static readonly Dictionary<ElementType, Color> ElementColors = new Dictionary<ElementType, Color>
    {
        { ElementType.Flame,     Color.red },
        { ElementType.Water,     Color.cyan },
        { ElementType.Wind,      Color.green },
        { ElementType.Earth,     new Color(115f/255f, 63f/255f, 43f/255f) },
        { ElementType.Lightning, new Color(113f/255f, 187f/255f, 230f/255f) },
        { ElementType.Dark,      Color.clear },
        { ElementType.Light,     new Color(255f/255f, 252f/255f, 162f/255f) }
    };

    private void Awake()
    {
        uiController = UIController.Instance;
        mainCam = Camera.main;
        rectTrs = GetComponent<RectTransform>();
        DamageText = GetComponent<TMP_Text>();
    }

    public override void Show()
    {
        timer = 0f;
        uiController.OnUIUpdate += UpdateUI;

        Vector2 screenPos = mainCam.WorldToScreenPoint(TargetTrs.position + Offset);
        rectTrs.position = screenPos;

        DamageText.color = GetElementColor();
        rectTrs.SetAsFirstSibling();
    }

    public override void Hide()
    {
        uiController.OnUIUpdate -= UpdateUI;

        Sender = null;
        Target = null;
        TargetTrs = null;

        timer = 0f;
    }

    public override void UpdateUI()
    {
        if (TargetTrs == null)
            return;

        rectTrs.position += Vector3.up * (MOVE_SPEED * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= LIFETIME)
        {
            OnUIImpact?.Invoke();
        }
    }

    private Color GetElementColor()
    {
        return ElementColors.TryGetValue(ElementType, out var color) 
            ? color 
            : Color.white;
    }
}
