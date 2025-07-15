using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged_addMenu;
    }

    private void OnGameStateChanged_addMenu(GameState state)
    {
        if (state == GameState.Victory)
        {
            GameVictory();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Debug.Log($"GAME QUITED");
        Application.Quit();
    }

    public void GameVictory()
    {
        SceneManager.LoadScene("Victory");
    }

}
