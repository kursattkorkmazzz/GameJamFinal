using UnityEngine;
using UnityEngine.Pool;

public class WeaponManager : MonoBehaviour
{
    public GameObject weaponHolder;
    public BaseWeapon currentWeapon;

    public RangedWeaponDataSO rangedWeapon;

    private Transform playerTransform;


    private ObjectPool<GameObject> bulletPool;


    private float lastFired;
    private float msBetweenAttack;

    void Start()
    {

        lastFired = 0;
        msBetweenAttack = 1000;

        playerTransform = GetComponent<Transform>();

        bulletPool = new ObjectPool<GameObject>(CreateBullet, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, 50);

    }


    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Attack();
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentWeapon is RangedWeapon rangedWeapon)
            {
                rangedWeapon.Reload();
            }
        }
    }

    public void Attack()
    {

      
        if (currentWeapon == null) return;

        if (Time.time - lastFired < (msBetweenAttack / 1000))
        {

            return;
        }
        lastFired = Time.time;




        // Attack function for Ranged Weapons.
        if (currentWeapon is RangedWeapon rangedWeapon)
        {

            bool isFired = rangedWeapon.Attack();
            if (isFired)
            {

                Vector3 bulletOffset = rangedWeapon.weaponData.bulletSpawnPoint;
                bulletOffset.x = bulletOffset.x * playerTransform.localScale.x;

                GameObject bullet = bulletPool.Get();
                bullet.transform.position = playerTransform.position + bulletOffset;
                Bullet bulletComponent = bullet.GetComponent<Bullet>();

                if (bulletComponent != null)
                {
                    bulletComponent.onCollisionDetected += BulletComponent_OnCollisionDetected;
                    bulletComponent.Fire(rangedWeapon.weaponData.bulletSpeed, rangedWeapon.weaponData.damage, playerTransform.localScale.x);
                }
            }

        }

    }

    public void DropWeapon()
    {
        if (currentWeapon is RangedWeapon rangedWeapon)
        {
            RangedWeaponDataSO weaponData = currentWeapon.WeaponData as RangedWeaponDataSO;
            weaponData.ammoAmount += rangedWeapon.currentAmmo;
        }

        currentWeapon = null;
        UpdateWeaponObject();
    }


    public void TakeWeapon(WeaponDataSO newWeapon)
    {
        if (newWeapon != null)
        {
            DropWeapon();

            if (newWeapon.gameObjectPrefab.TryGetComponent<BaseWeapon>(out BaseWeapon weaponBase))
            {
                currentWeapon = weaponBase;
                UpdateWeaponObject();
                msBetweenAttack = 1000 / currentWeapon.WeaponData.fireAmountPerSecond;
            }
            else
            {
                Debug.Log("The given item is not a weapon: It is not contain a tyoe of BaseWeapon");
            }
        }

    }


    private void UpdateWeaponObject()
    {
        int childCount = weaponHolder.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(weaponHolder.transform.GetChild(i).gameObject);
        }


        if (currentWeapon == null)
        {
            return;
        }

        Vector3 positionOffset = currentWeapon.WeaponData.weaponHoldPosition;
        positionOffset.x = positionOffset.x * playerTransform.localScale.x;

        GameObject weapon = Instantiate(currentWeapon.WeaponData.gameObjectPrefab, weaponHolder.transform.position + positionOffset, currentWeapon.WeaponData.gameObjectPrefab.transform.rotation, weaponHolder.transform);
        if (weapon.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            Destroy(rb);
        }
        if (weapon.TryGetComponent<BoxCollider2D>(out BoxCollider2D col))
        {
            Destroy(col);
        }
    }



    #region Bullet Pooling Methods

    private GameObject CreateBullet()
    {
        RangedWeapon rangedWeapon = currentWeapon as RangedWeapon;

        Vector3 bulletOffset = rangedWeapon.weaponData.bulletSpawnPoint;
        bulletOffset.x = bulletOffset.x * playerTransform.localScale.x;

        GameObject bullet = Instantiate(rangedWeapon.weaponData.bulletPrefab, playerTransform.position + bulletOffset, rangedWeapon.weaponData.bulletPrefab.transform.rotation);
        Bullet bulletComponent = bullet.GetComponent<Bullet>();

        if (bulletComponent != null)
        {
            bulletComponent.onCollisionDetected += BulletComponent_OnCollisionDetected;

            bulletComponent.Fire(rangedWeapon.weaponData.bulletSpeed, rangedWeapon.weaponData.damage, playerTransform.localScale.x);
        }


        return bullet;

    }

    private void BulletComponent_OnCollisionDetected(GameObject bullet)
    {
        

        Bullet bulletComponent = bullet.GetComponent<Bullet>();

        if (bulletComponent != null)
        {
            bulletComponent.onCollisionDetected -= BulletComponent_OnCollisionDetected;
        }
        try
        {
            bulletPool.Release(bullet);
        }
        #pragma warning disable CS0168 // Variable is declared but never used
        catch (System.InvalidOperationException ioe)
        #pragma warning restore CS0168 // Variable is declared but never used
        {
            
            // This error is not critical. So, i just skip it.
        }
            
       
    }

    void OnReturnedToPool(GameObject bullet)
    {
        bullet.gameObject.SetActive(false);
    }
    void OnTakeFromPool(GameObject bullet)
    {
        if (bullet.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.velocity = new Vector2(0,0);
        }
        bullet.gameObject.SetActive(true);
    }
    void OnDestroyPoolObject(GameObject bullet)
    {
        Destroy(bullet);
    }

    #endregion
}
