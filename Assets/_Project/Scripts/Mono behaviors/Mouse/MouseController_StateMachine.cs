using System;

public partial class MouseController
{
    private MouseState craftBehaviour;

    private MouseState gameplayBehaviour;
    private MouseState onCraftBehaviour;
    private MouseStateMachine mouseStateMachine;

    private void SetupMachineState()
    {
        gameplayBehaviour = new MouseGameplayState(this, onClick: CheckForDragBegin, onRelease: CheckForDragRelease, onExit: RemoveCachedDraggable);
        onCraftBehaviour = new MouseCraftUIState(this, onClick: ClickOnCraftHUD);

        mouseStateMachine = new MouseStateMachine();
        mouseStateMachine.Initialize(gameplayBehaviour);
    }
}

public abstract class MouseState
{
    protected MouseController MouseController;

    protected Action onEnter, onExit, onClick, onRelease;

    protected MouseState (MouseController mouseController, 
        Action onEnter = null, 
        Action onExit = null, 
        Action onClick = null, 
        Action onRelease = null)
    {
        MouseController = mouseController;

        this.onEnter = onEnter;
        this.onExit = onExit;
        this.onClick = onClick;
        this.onRelease = onRelease;
    }

    public string Name => GetType().ToString();

    public virtual void Click() => onClick?.Invoke();
    public virtual void Release() => onRelease?.Invoke();

    public virtual void Enter() => onEnter?.Invoke();
    public virtual void Exit() => onEnter?.Invoke();
}

public class MouseCraftUIState : MouseState
{
    public MouseCraftUIState (MouseController mouseController, 
        Action onEnter = null, 
        Action onExit = null, 
        Action onClick = null, 
        Action onRelease = null) : base(mouseController, onEnter, onExit, onClick, onRelease) { }
}

public class MouseGameplayState : MouseState
{
    public MouseGameplayState (MouseController mouseController,
        Action onEnter = null,
        Action onExit = null,
        Action onClick = null,
        Action onRelease = null) : base(mouseController, onEnter, onExit, onClick, onRelease) { }

    public override void Exit()
        => onExit?.Invoke();
}

public class MouseStateMachine
{
    public MouseState CurrentState { get; set; }

    public void Initialize (MouseState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void ChangeState (MouseState newState)
    {
        CurrentState.Exit();

        CurrentState = newState;
        CurrentState.Enter();
    }
}