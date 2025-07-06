using UnityEngine;

public class KidChaseState : KidState
{
    public KidChaseState(KidsAI kid) : base(kid) { }

    public bool canChase_Range;
    public bool canCaught_Range;


    public override void Enter()
    {
        kid.agent.isStopped = false;
        kid.agent.speed = kid.chaseSpeed;
    }

    public override void Update()
    {
        kid.agent.SetDestination(kid.player.position);
        IsPlayerInRange();          
    }

    public override void Exit()
    {
        kid.agent.speed = kid.patrolSpeed;
    }


    public void IsPlayerInRange()
    {
        float distance = Vector3.Distance(kid.fovPoint.position, kid.player.position);
        if (distance < kid.viewDistance)  // is in Chase Range
        {
            if (distance < kid.catctDistance)  // is in Catch Range
            {
                kid.SwitchState(new KidCatchState(kid));
            }
        }
        else                        // is out of kid sight Range
        {
            kid.SwitchState(new KidSearchState(kid));
        }
    }
}
