using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public abstract float Health { get; set; }
    public abstract float MaxHealth { get; set; }

    public abstract void Damage(int amount);
    public abstract void Die();
    public abstract void Heal(int amount);


    #region Events
    public delegate void onEnemyDie();
    public virtual event onEnemyDie OnEnemyDie;
    #endregion
}
