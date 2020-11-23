using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
    public string playerName = "Player One";

    public GameState gameState;
    public Difficulty difficulty;

    public int score = 0;
    public TMP_Dropdown difficultyDropdown;
    public TMP_InputField playerNameInput;

    private void Start()
    {
        gameState = GameState.TITLE;
        GameEvents.ReportGameStateChange(gameState);
        GameEvents.ReportDifficultyChange(difficulty);
        //Score(0);
    }

    public void ChangeDifficulty(int _diff)
    {
        //Method One
        difficulty = (Difficulty)_diff;

        //Method Two... too long.
        string st = difficultyDropdown.options[_diff].text;
    }

    public void UpdatePlayerName(string _name)
    {
        playerName = _name;
    }

    public void Score (int _amount)
    {
        score += _amount;
        UIManager.instance.scoreText.text = playerName + "'s score: " + score.ToString();
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

    private void Update()
    {
       
        if(Input.GetKeyDown(KeyCode.G))
        {
            gameState = GameState.INGAME;
            GameEvents.ReportGameStateChange(gameState);
        }
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        // Start loading the scene
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync("MainGame", LoadSceneMode.Single);
        // Wait until the level finish loading
        while (!asyncLoadLevel.isDone)
            yield return null;
        // Wait a frame so every Awake and Start method is called
        yield return new WaitForEndOfFrame();

        gameState = GameState.INGAME;
        GameEvents.ReportDifficultyChange(difficulty);
        GameEvents.ReportGameStateChange(gameState);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
