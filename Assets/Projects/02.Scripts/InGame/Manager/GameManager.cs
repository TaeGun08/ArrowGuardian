using System.Collections;
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
    
    public float TotalExp { get; private set; }
    public float CurrentExp { get; private set; }
    
    public void SetGameState(GameState state)
    {
        switch (state)
        {
            case GameState.GameStart:
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

        if (TotalExp > CurrentExp) return;
        float sumExp = CurrentExp - TotalExp;
        CurrentExp = sumExp;
        TotalExp *= 0.1f;
    }
}