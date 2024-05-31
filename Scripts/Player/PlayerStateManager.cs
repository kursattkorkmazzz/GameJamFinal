using UnityEngine;
using DesignPatterns.FiniteStateMachine;

public class PlayerStateManager : FiniteStateMachine
{

    public PlayerState STATE_IDLE;
    public PlayerState STATE_RUN;
    public PlayerState STATE_JUMP;

    public PlayerStateManager(PlayerManager playerManager)
    {
        STATE_IDLE = new PlayerIdleState(playerManager);
        STATE_RUN = new PlayerRunState(playerManager);
        STATE_JUMP = new PlayerJumpState(playerManager);
    }



    public void Initialization(PlayerState initialState)
    {
        base.Initialization(initialState);
    }

    public  void ChangeState(PlayerState newState)
    {
        base.ChangeState(newState);
    }

    public PlayerState GetCurrrentState()
    {
        return (PlayerState)currentState;
    }
   
}
