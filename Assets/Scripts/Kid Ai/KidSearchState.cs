using UnityEngine;

public class KidSearchState : KidState
{
    float searchTime = 3f;
    float timer = 0f;
    
    public KidSearchState(KidsAI kid) : base(kid) { }
  
    public override void Enter()
    {
        searchTime = kid.searchTime;
        kid.agent.isStopped = true;
        timer = 0f;
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= searchTime)
        {
            kid.SwitchState(new KidPatrolState(kid));
        }
    }

}
