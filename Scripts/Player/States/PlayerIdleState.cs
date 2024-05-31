using UnityEngine;

public class PlayerIdleState : PlayerState
{

    
    public PlayerIdleState(PlayerManager playerManager) : base(playerManager){}

    public override void Enter()
    {
        playerManager.CurrentRunSpeed = 0;
        playerManager.playerAnimator.SetFloat("xSpeed", playerManager.CurrentRunSpeed);
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
       
    }
}
