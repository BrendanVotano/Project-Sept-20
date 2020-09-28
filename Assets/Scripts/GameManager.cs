using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    TITLE,
    INGAME,
    PAUSED,
    GAMEOVER,
}

public enum Difficulty
{
    EASY, MEDIUM, HARD
}

public class GameManager : Singleton<GameManager>
{
    public GameState gameState;
    public Difficulty difficulty;

    public int score = 0;

    private void Start()
    {
        gameState = GameState.TITLE;
        GameEvents.ReportDifficultyChange(difficulty);
    }

    public void Score (int _amount)
    {
        score += _amount;
    }


    private void OnEnable()
    {
        GameEvents.OnEnemyHit += OnEnemyHit;
        GameEvents.OnEnemyDied += OnEnemyDied;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyHit -= OnEnemyHit;
        GameEvents.OnEnemyDied -= OnEnemyDied;
    }

    void OnEnemyHit()
    {
        Score(1);
    }

    void OnEnemyDied(Enemy _enemy)
    {
        Score(10);
    }
}
