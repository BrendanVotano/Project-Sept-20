using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action              OnEnemyHit = null;
    public static event Action<Enemy>       OnEnemyDied = null;

    public static event Action<Difficulty>  OnDifficultyChange = null;
    public static event Action<GameState>   OnGameStateChange = null;

    public static void ReportEnemyHit()
    {
        OnEnemyHit?.Invoke();
        //Debug.Log("Enemy Hit Event");
    }

    public static void ReportEnemyDied(Enemy _enemy)
    {
        OnEnemyDied?.Invoke(_enemy);
        //Debug.Log(_enemy.name + " Died");
    }

    public static void ReportDifficultyChange(Difficulty _difficulty)
    {
        OnDifficultyChange?.Invoke(_difficulty);
    }

    public static void ReportGameStateChange(GameState _gameState)
    {
        OnGameStateChange?.Invoke(_gameState);
    }

}
