public class BasicState : PlayerState
{
    public BasicState (Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) { }
    public override PlayerStateEnum StateEnum => PlayerStateEnum.Basic;
}