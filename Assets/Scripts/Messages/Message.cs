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

    private void Start()
    {
        // uiMessageAnimator = uiMessageText.GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiMessageText.text = message;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiMessageText.text = "";
        }
    }


    // IEnumerator RemoveMessage()
    // {
    //     yield return new WaitForSeconds(fadeTimer);
    //     if (uiMessageText.text == message)
    //     {
    //         uiMessageText.text = "";
    //     }

    // } 



}
