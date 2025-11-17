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
    }

    public float TotalExp { get; private set; } = 5;
    public float CurrentExp { get; private set; }

    private WaveController waveController;
    private AbilityDraft abilityDraft;
    public UnitStatsUI UnitStatsUI { get; set; }

    public Action DraftAction;

    private void Start()
    {
        SetGameState(GameState.GameStart);
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;
        
        if (Input.GetKeyDown(KeyCode.P)) Time.timeScale = 3;
        else if (Input.GetKeyUp(KeyCode.P)) Time.timeScale = 1;
    }

    public void SetGameState(GameState state)
    {
        switch (state)
        {
            case GameState.GameStart:
                waveController = gameObject.AddComponent<WaveController>();
                waveController.StartWave();
                break;
            case GameState.Playing:
                Time.timeScale = 1;
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                break;
            case GameState.GameOver:
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
}