using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour , IInteractable
{

    enum PigState
    {
        idle,
        posses
    }

    PigState pigState = PigState.idle;

    [SerializeField] int cornEated = 0;
    int maxCornEat = 3;
    bool canPose = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (pigState)
        {
            case PigState.idle:
                Idle();
                break;
            case PigState.posses:
                Posses();
                break;

        }
        if (cornEated >= maxCornEat)
        {
            canPose = true;
            cornEated = 0;
        }


    }

    void ChangeState(PigState newState)
    {
        pigState = newState;
    }

    void Idle()
    {

    }

    void Posses()
    {
        canPose = false;

    }

    public void Interact()
    {
        
    }
}
