using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public enum GameState
{
    Intro,
    FreeRoam,
    KidArrivalCutscene,
    DayPlaying,
    NightRecap,
    GameOver,
    Victory
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    public int currentDay = 1;
    public int maxDays = 3;
    public GameState gameState = GameState.Intro;

    public float freeRoamTime = 60f;
    private float freeRoamTimer;

    public bool isCaught = false;
    public int catchCount = 0;
    public int maxCatches = 3;

    public Action<GameState> OnGameStateChanged;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        ChangeState(GameState.Intro);
    }

    void Update()
    {
        if (gameState == GameState.FreeRoam)
        {
            freeRoamTimer -= Time.deltaTime;
            if (freeRoamTimer <= 0)
            {
                TriggerKidArrival();
            }
        }
    }

    // 🔄 Call this to switch state cleanly
    public void ChangeState(GameState newState)
    {
        gameState = newState;
        Debug.Log($"Game State changed to: {newState}");

        OnGameStateChanged?.Invoke(newState);

        switch (newState)
        {
            case GameState.Intro:
                StartIntroSequence();
                break;

            case GameState.FreeRoam:
                StartFreeRoam();
                break;

            case GameState.KidArrivalCutscene:
                StartCutscene();
                break;

            case GameState.DayPlaying:
                StartDayGameplay();
                break;

            case GameState.NightRecap:
                ShowNightRecap();
                break;

            case GameState.GameOver:
                TriggerGameOver();
                break;

            case GameState.Victory:
                TriggerVictory();
                break;
        }
    }

    private void StartDayGameplay()
    {
        throw new NotImplementedException();
    }

    private void StartIntroSequence()
    {
        // Trigger your intro comic/cutscene
        // Then transition to FreeRoam after a few seconds
        Invoke(nameof(StartFreeRoam), 5f);
    }

    private void StartFreeRoam()
    {
        ChangeState(GameState.FreeRoam);
        freeRoamTimer = freeRoamTime;
    }

    private void TriggerKidArrival()
    {
        ChangeState(GameState.KidArrivalCutscene);
    }

    private void StartCutscene()
    {
        // Cutscene logic can subscribe to OnGameStateChanged or call back to:
        Invoke(nameof(BeginDayGameplay), 5f);
    }

    private void BeginDayGameplay()
    {
        ChangeState(GameState.DayPlaying);
    }

    public void EndDay()
    {
        if (currentDay >= maxDays)
        {
            TriggerGameOver(); // Or CheckWinCondition();
        }
        else
        {
            currentDay++;
            ChangeState(GameState.NightRecap);
        }
    }

    private void ShowNightRecap()
    {
        // Show comic panel / diary, then start next day
        Invoke(nameof(BeginDayGameplay), 5f);
    }

    public void PlayerCaught()
    {
        catchCount++;
        if (catchCount >= maxCatches)
        {
            TriggerGameOver();
        }
    }

    public void PlayerEscaped()
    {
        ChangeState(GameState.Victory);
    }

    private void TriggerGameOver()
    {
        Debug.Log("You were caught too many times!");
        ChangeState(GameState.GameOver);
        // Load fail screen or scene
    }

    private void TriggerVictory()
    {
        Debug.Log("You escaped the farm!");
        // Load win screen or trigger cutscene
    }
}
