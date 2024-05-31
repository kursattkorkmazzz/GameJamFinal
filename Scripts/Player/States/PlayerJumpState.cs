using UnityEngine;

public class PlayerJumpState : PlayerState
{

    
    public PlayerJumpState(PlayerManager playerManager) : base(playerManager){}

    public override void Enter()
    {

        playerManager.rb.velocity = new Vector2(playerManager.rb.velocity.x, playerManager.transform.up.y * playerManager.JumpSpeed);

        playerManager.isGrounded = false;
        
    }

    public override void Update()
    {
        playerManager.playerAnimator.SetBool("Jumped", true);
    }

    public override void Exit()
    {
        playerManager.playerAnimator.SetBool("Jumped", false);

    }
}
