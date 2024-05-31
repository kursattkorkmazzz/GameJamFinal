using UnityEngine;

public class Ghost : Enemy
{

    public float health;
    public override float Health { get => health; set => health = value; }

    public float maxHealth;
    public override float MaxHealth { get => maxHealth; set => maxHealth = value; }

    [Header("Ghost Stats")]
    public float speed;
    public float minTargetDistance = 0.5f;
    [Header("Trigger")]
    public Vector2 triggerOffset;
    public float triggerRadius;



    public override event onEnemyDie OnEnemyDie;

    void Start()
    {
        
    }

    
    void Update()
    {
        
        TriggerCheck();
    }


    
    private void TriggerCheck()
    {
        Vector2 position = new Vector2(transform.position.x + triggerOffset.x, transform.position.y + triggerOffset.y);
        Collider2D[] cols = Physics2D.OverlapCircleAll(position, triggerRadius);
        foreach (Collider2D col in cols)
        {
            if (col.tag == "Player")
            {
                // Setting the looking of enemy.
                if (col.transform.position.x - transform.position.x < 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                Chase(col.GetComponent<Transform>());
            }
        }


    }

    private void Chase(Transform target)
    {

        bool isChase = Vector3.Distance(target.position ,transform.position) > minTargetDistance;
        if (isChase)
        {
            Vector3 direction = CalculateDirection(target.position, transform.position);

            transform.position += direction * speed * Time.deltaTime;
        }

    }


    private Vector3 CalculateDirection(Vector3 pointA, Vector3 pointB)
    {

        Vector3 direction = pointA - pointB;
        Vector3 unitVector = direction.normalized;
        return unitVector;

    }


    #region IDamageabla methods
    public override void Damage(int amount)
    {
       if(amount > 0) Health -= amount;
        if (Health <= 0) Die();
    }

    public override void Die()
    {
        OnEnemyDie?.Invoke();
        Destroy(gameObject);
    }

    public override void Heal(int amount)
    {
        if(amount > 0)Health += amount;
    }


    #endregion
}
