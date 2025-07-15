// 🎬 CutsceneController.cs
// Manages the kid arrival cutscene and transitions to Day 1 gameplay

using UnityEngine;
using System.Collections;
using TMPro;

public class CutsceneController : MonoBehaviour
{
    public Transform uncle;
    public Transform[] kids;
    public Transform[] kidArrivalPositions;
    public Transform uncleTargetPosition;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;


    public Animator gateAnimator;
    public float cutsceneDuration = 5f;

    public GameObject dialogueUI;
    public string[] dialogueLines;

    [SerializeField] Camera cutSceneCamera;

    
    void Start()
    {
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    void OnGameStateChanged(GameState state)
    {
        if (state == GameState.KidArrivalCutscene)
        {
            StartCoroutine(PlayCutscene());
        }
    }

    IEnumerator PlayCutscene()
    {

        cutSceneCamera.enabled = true;

        // uncle.position = uncleTargetPosition.position;
        uncle.position = Vector3.SmoothDamp(uncle.position, uncleTargetPosition.position, ref velocity, smoothTime);

        for (int i = 0; i < kids.Length; i++)
        {
           // kids[i].position = kidArrivalPositions[i].position;
            kids[i].position =  Vector3.SmoothDamp(kids[i].position, kidArrivalPositions[i].position, ref velocity, smoothTime);

        }

        gateAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(1.5f);

        if (dialogueUI != null && dialogueLines.Length > 0)
        {
            dialogueUI.SetActive(true);
            foreach (string line in dialogueLines)
            {
                dialogueUI.GetComponent<TextMeshProUGUI>().text = line;
                yield return new WaitForSeconds(3f);
            }
            dialogueUI.SetActive(false);
        }

        cutSceneCamera.enabled = false;
        yield return new WaitForSeconds(1f);
        gateAnimator.SetTrigger("Close");

        yield return new WaitForSeconds(cutsceneDuration);

        GameManager.Instance.ChangeState(GameState.DayPlaying);
    }
}
