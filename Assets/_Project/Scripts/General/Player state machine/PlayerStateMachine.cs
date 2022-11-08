﻿public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }

    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        CurrentState.Exit();

        CurrentState = newState;
        newState.Enter();
    }
}