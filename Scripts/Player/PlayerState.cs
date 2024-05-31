using UnityEngine;
using DesignPatterns.FiniteStateMachine;

public abstract class PlayerState : State
{

    protected PlayerManager playerManager;

    public PlayerState(PlayerManager playerManager) : base()
    {
        if (playerManager != null)
        {
            this.playerManager = playerManager;
        }
       
    }

   
}
