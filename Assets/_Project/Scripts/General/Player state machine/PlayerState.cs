
public enum PlayerStateEnum
{
    Basic
}

public abstract class PlayerState
{
    protected Player player;
    protected PlayerStateMachine playerStateMachine;

    public abstract PlayerStateEnum StateEnum { get; } 
    
    public PlayerState (Player player, PlayerStateMachine playerStateMachine)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void Update()
    {
        if (player.CanMove())
            player.Move(player.currentDirection);
    }
}