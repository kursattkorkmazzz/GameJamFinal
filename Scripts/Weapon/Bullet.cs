using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float baseDamage;
    private float baseSpeed;

    public delegate void OnCollisionDetected(GameObject bullet);
    public event OnCollisionDetected onCollisionDetected;

    private Rigidbody2D rb;


    public void Fire(float bulletSpeed, float baseDamage, float direction)
    {
        this.baseDamage = baseDamage;
        this.baseSpeed = bulletSpeed;





        rb = GetComponent<Rigidbody2D>();

        
        rb.AddForceX(direction * bulletSpeed * Time.deltaTime, ForceMode2D.Impulse);
        rb.AddForceY(Time.deltaTime * 500, ForceMode2D.Impulse);
        StartCoroutine("AutomaticRemove");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;

        if (damageable != null)
        {
            /*
                Damage calculation
            */
            float bulletSpeedEfector = rb.velocityX / baseSpeed;
            damageable.Damage((int)baseDamage);
        
        }


        
        onCollisionDetected?.Invoke(gameObject);

    }



    System.Collections.IEnumerator AutomaticRemove()
    {
        yield return new WaitForSeconds(5);
        onCollisionDetected?.Invoke(gameObject);
    }
}
