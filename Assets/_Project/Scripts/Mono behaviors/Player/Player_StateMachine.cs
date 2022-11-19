public partial class Player
{
    private PlayerStateMachine stateMachine;
    private BasicState basicState;

    private void SetupStateMachine()
    {
        stateMachine = new PlayerStateMachine();

        basicState = new BasicState(this, stateMachine);

        stateMachine.Initialize(basicState);
    }
}