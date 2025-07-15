using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    [SerializeField] TextMeshProUGUI roamAroundTimerUi, currentTask, DestructionScore;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void UpdateRoamTimer(string freeRoamTimer)
    {
        roamAroundTimerUi.text = freeRoamTimer;
    }

    public void UpdateRoamTimer(float freeRoamTimer)
    {
        roamAroundTimerUi.text = ((int)freeRoamTimer).ToString();
    }

    public void UpdateTask(string task)
    {
        currentTask.text = task;
    }

    public void UpdateDestructionScore( string score)
    {
        DestructionScore.text = score;
    }

    public int GetDestructionScore()
    {
        return Int32.Parse(DestructionScore.text);
    }
}
