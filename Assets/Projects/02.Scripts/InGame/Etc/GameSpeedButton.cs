using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedButton : MonoBehaviour
{
    private GameManager gameManager;
    
    private Button button;
    private TMP_Text text;

    private int gameSpeedCount;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<TMP_Text>();
        ButtonEvent();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void ButtonEvent()
    {
        button.onClick.AddListener(() =>
        {
            gameManager.SetGameSpeed(GameSpeedChecker());
        });
    }

    private float GameSpeedChecker()
    {
        gameSpeedCount++;
        
        switch (gameSpeedCount)
        {
            case 1:
                text.text = $"x{1.5f}";
                return 1.5f;
            case 2:
                text.text = $"x{2f}";
                return 2f;
            case 3:
                text.text = $"x{3f}";
                return 3f;
            case 4:
                text.text = $"x{1f}";
                gameSpeedCount = 0;
                return 1f;
        }
        
        return 1;
    }
}
