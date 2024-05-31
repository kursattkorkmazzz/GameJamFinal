using UnityEngine;

public interface IDamageable
{
    float Health { get;  }
    float MaxHealth { get; }

    void Damage(int amount);
    void Heal(int amount);

    void Die();
}
