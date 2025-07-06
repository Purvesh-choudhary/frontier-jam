using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidCatchState : KidState
{
    public KidCatchState(KidsAI kid) : base(kid) { }

    public override void Enter()
    {
        kid.agent.isStopped = false;
        ChangePlayerLocation(kid.chickenHolder);
        kid.cageTransform.GetComponentInParent<BoxCollider>().isTrigger = true;
        kid.player.GetComponent<PlayerController>().enabled = false;
        kid.agent.SetDestination(kid.cageTransform.position);
    }
    public override void Update()
    {
        float distance = Vector3.Distance(kid.transform.position, kid.cageTransform.position);
        if (distance < kid.cageDistance)
        {
            kid.agent.isStopped = true;
            kid.isChickenInCage = true;
            ChangePlayerLocation(kid.cageTransform);
            kid.cageTransform.GetComponentInParent<BoxCollider>().isTrigger = false;
            kid.player.GetComponent<PlayerController>().enabled = true;

            kid.SwitchState(new KidPatrolState(kid));
        }
    }
    public override void Exit()
    {
        
    }

    void ChangePlayerLocation(Transform location)
    {
        kid.player.gameObject.transform.SetParent(location);
        kid.player.localPosition = Vector3.zero;
    }
    

}
