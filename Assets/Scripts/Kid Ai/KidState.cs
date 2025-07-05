public abstract class KidState
{

    protected KidsAI kid;

    public KidState(KidsAI kid)
    {
        this.kid = kid;
    }

    public virtual void Enter()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void Exit()
    {
    }
    

}
