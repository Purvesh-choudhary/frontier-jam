using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Message : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI uiMessageText;
    [TextArea(1, 4)]
    [SerializeField] string message;
    [SerializeField] float fadeTimer = 5f;

    // void Start()
    // {
    //     GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
    // }

    // void OnDisable()
    // {
    //     GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    // }


    // void OnGameStateChanged(GameState state)
    // {
    //     if (state == GameState.KidArrivalCutscene)
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            uiMessageText.text = message;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            uiMessageText.text = "";
        }
    }
}
