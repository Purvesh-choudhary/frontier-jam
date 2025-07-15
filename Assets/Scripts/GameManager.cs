// 🧠 GameManager.cs
// Manages overall game state, transitions, win/loss, day cycle

using UnityEngine;
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
            UiManager.Instance.UpdateRoamTimer(freeRoamTimer);
            if (freeRoamTimer <= 0)
            {   
                
                TriggerKidArrival();
            }
        }
    }

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
        UiManager.Instance.UpdateTask("Day 1 - Operation: Feathers Fly");
        
    }

    private void StartIntroSequence()
    {
        // Play comic or intro cutscene, then begin free roam
        Invoke(nameof(StartFreeRoam), 5f);
    }

    private void StartFreeRoam()
    {
        freeRoamTimer = freeRoamTime;
        gameState = GameState.FreeRoam;
    }

    private void TriggerKidArrival()
    {
        UiManager.Instance.UpdateRoamTimer("Kids Are Coming !");
        ChangeState(GameState.KidArrivalCutscene);
    }

    private void StartCutscene()
    {
        // Use Unity Timeline or a coroutine sequence
        // Invoke(nameof(BeginDayGameplay), 5f);
    }

    private void BeginDayGameplay()
    {
        ChangeState(GameState.DayPlaying);
    }

    public void EndDay()
    {
        if (currentDay >= maxDays)
        {
            TriggerGameOver(); // Or TriggerVictory if escaped
        }
        else
        {
            currentDay++;
            ChangeState(GameState.NightRecap);
        }
    }

    private void ShowNightRecap()
    {
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
        Debug.Log("GAME OVER: You were caught too many times!");
    }

    private void TriggerVictory()
    {
        Debug.Log("VICTORY: You escaped the farm!");
    }
}
