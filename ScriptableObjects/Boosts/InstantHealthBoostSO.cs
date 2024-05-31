using UnityEngine;

[CreateAssetMenu(fileName = "New Instant Health Boost", menuName = "Boosts/Instant Health Boost")]
public class InstantHealthBoostSO : BoostDataSO
{


    public string itemId;
    public override string ItemId { get => itemId; set { } }

    public Sprite iconUI;
    public override Sprite IconUI { get => iconUI; set => iconUI = value; }


    public int amount;
    public override int Amount { get => amount; set => amount = value; }

    public int instantHealthAmount;
    public override void Use(GameObject target)
    {
        
        if (target.GetComponent<PlayerManager>() is IDamageable healable)
        {
            
            int healAmount = (int)(healable.MaxHealth * (instantHealthAmount / 100.0));
            healable.Heal(healAmount);
            
        }
    }
}
