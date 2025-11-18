using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    private GameManager gameManager;
    private Button button;
    
    [Header("MenuUI")]
    [SerializeField] private GameObject menu;

    private void Awake()
    {
        button = GetComponent<Button>();
        
        button.onClick.AddListener(() =>
        {
            gameManager.SetGameState(GameManager.GameState.Paused);
        });
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
}
