using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidCatchState : KidState
{
    public KidCatchState(KidsAI kid) : base(kid) { }

    public override void Enter()
    {
        kid.agent.isStopped = false;
        kid.animator.SetTrigger("Catch");

        kid.player.GetComponent<PlayerController>().enabled = false;
        kid.cageTransform.GetComponentInParent<Rigidbody>().isKinematic = true;
        Collider[] cages = kid.cageTransform.parent.GetComponentsInChildren<Collider>();
        foreach (Collider cage in cages)
        {
            cage.enabled = false;
        }

        kid.player.gameObject.transform.SetParent(kid.chickenHolder);
        kid.player.localPosition = Vector3.zero;

        kid.agent.SetDestination(kid.cageTransform.position);
    }
    public override void Update()
    {
        float distance = Vector3.Distance(kid.transform.position, kid.cageTransform.position);
        if (distance < kid.cageDistance)
        {
            kid.agent.isStopped = true;

            kid.player.SetParent(null); 
            kid.player.gameObject.transform.localPosition = kid.cageTransform.position;




            kid.player.GetComponent<PlayerController>().enabled = true;

            Collider[] cages = kid.cageTransform.parent.GetComponentsInChildren<Collider>();

            foreach (Collider cage in cages)
            {
                cage.enabled = true;
            }
            kid.cageTransform.GetComponentInParent<Rigidbody>().isKinematic = false;

            kid.isChickenInCage = true;
            kid.SwitchState(new KidPatrolState(kid));
        }
    }
    public override void Exit()
    {

    }

    void ChangePlayerLocation(Transform location)
    {
       

    }


}
