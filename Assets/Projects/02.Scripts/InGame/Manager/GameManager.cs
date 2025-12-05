using System;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    public enum GameState
    {
        GameStart,
        Playing,
        Paused,
        GameOver,
        GameClear,
    }

    public float TotalExp { get; private set; } = 5;
    public float CurrentExp { get; private set; }

    private float totalHealth = 2000f;
    private float health;
    
    private GameState currentState;

    private WaveController waveController;
    private AbilityDraft abilityDraft;
    public UnitStatsUI UnitStatsUI { get; set; }

    public Action DraftAction;

    private float gameSpeed = 1f;
    
    private void Start()
    {
        health = totalHealth;
        SetGameState(GameState.GameStart);
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;
        
        if (Input.GetKeyDown(KeyCode.U)) LevelUp();
    }

    private void LevelUp()
    {
        CurrentExp = TotalExp;
        SetExp(1);
    }

    public void SetGameState(GameState state)
    {
        if (currentState is GameState.GameClear or GameState.GameOver) return;
        
        currentState = state;

        switch (currentState)
        {
            case GameState.GameStart:
                waveController = gameObject.AddComponent<WaveController>();
                waveController.StartWave();
                break;
            case GameState.Playing:
                Time.timeScale = gameSpeed;
                break;
            case GameState.Paused or GameState.GameOver or GameState.GameClear:
                Time.timeScale = 0f;
                break;
        }
    }

    public void SetExp(float exp)
    {
        CurrentExp += exp;

        if (TotalExp <= CurrentExp)
        {
            DraftAction?.Invoke();
            float diffExp = CurrentExp - TotalExp;
            CurrentExp = diffExp;
            TotalExp += (TotalExp * 0.15f);
            SetGameState(GameState.Paused);
        }

        UnitStatsUI.SetExpBar(CurrentExp, TotalExp);
    }

    public void SetHealth(float hp)
    {
        health -= hp;
        Wall.Instance.HitWall();
        
        if (health <= 0)
        {
            SetGameState(GameState.GameOver);
        }
        
        UnitStatsUI.SetHealthText($"{health}");
    }

    public void SetGameSpeed(float speed)
    {
        gameSpeed = speed;
        SetGameState(GameState.Playing);
    }
}