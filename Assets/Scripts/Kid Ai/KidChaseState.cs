public class KidChaseState : KidState
{
    public KidChaseState(KidsAI kid) : base(kid) { }

    public override void Enter()
    {
        kid.agent.isStopped = false;
        kid.agent.speed = kid.chaseSpeed;
    }

    public override void Update()
    {
        kid.agent.SetDestination(kid.player.position);

        if (!kid.CanSeePlayer())
        {
            kid.SwitchState(new KidSearchState(kid));
        }
    }

    public override void Exit()
    {
        kid.agent.speed = kid.patrolSpeed;
    }
}
