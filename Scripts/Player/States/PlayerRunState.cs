using UnityEngine;

public class PlayerRunState : PlayerState
{

    
    public PlayerRunState(PlayerManager playerManager) : base(playerManager){}

    public override void Enter()
    {
       
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {

        

        playerManager.playerAnimator.SetFloat("xSpeed",(float)playerManager.CurrentRunSpeed);

        Vector2 xVelocity = new Vector2( playerManager.CurrentRunSpeed * playerManager.transform.localScale.x ,playerManager.rb.velocity.y);

        playerManager.rb.velocity = xVelocity;
        

    }

  
}
