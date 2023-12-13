public interface IState
{
    public void OnEnter();
    public void StateUpdate();
    public void OnExit();
}