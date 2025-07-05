using UnityEngine;

public class KidPatrolState : KidState
{
    int currentWaypoint = 0;
    public KidPatrolState(KidsAI kid) : base(kid) { }

    public override void Enter()
    {
        kid.agent.isStopped = false;
        kid.agent.speed = kid.patrolSpeed;
        kid.agent.SetDestination(kid.patrolPoints[currentWaypoint].position);
    }

    public override void Update()
    {
        if (kid.CanSeePlayer())
        {
            kid.SwitchState(new KidChaseState(kid));
            return;
        }

        // Move to next waypoint if reached
        if (!kid.agent.pathPending && kid.agent.remainingDistance < 0.5f)
        {
            currentWaypoint = (currentWaypoint + 1) % kid.patrolPoints.Length;
            kid.agent.SetDestination(kid.patrolPoints[currentWaypoint].position);
        }
    }


}
