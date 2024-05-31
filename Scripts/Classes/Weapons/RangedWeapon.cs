using System;
public abstract class
    RangedWeapon : BaseWeapon
{

    public RangedWeaponDataSO weaponData;
    public override WeaponDataSO WeaponData { get => weaponData; set {
            if(value is RangedWeaponDataSO rangedWeaponData)
            {
                weaponData = rangedWeaponData;
            }
        } }


    public int currentAmmo;
    




    /// <summary>
    /// Decreases current ammo one by if there is current ammo.
    /// </summary>
    /// <returns>true if there are current ammo, false otherwise.</returns>
    public virtual bool Attack()
    {
        if (currentAmmo > 0)
        {
            currentAmmo -= 1;
            return true;
        }
        return false;
    }


    /// <summary>
    /// Reloads the pistol's current ammo.
    /// </summary>
    /// <returns>true if there are ammos, false otherwise.</returns>
    public virtual bool Reload()
    {

        if (weaponData.ammoAmount <= 0)
        {
            return false;
        }
        int requiredAmmo = weaponData.maxMagSize - currentAmmo;

        if (weaponData.ammoAmount >= requiredAmmo)
        {
            currentAmmo += requiredAmmo;
            weaponData.ammoAmount -= requiredAmmo;
        }
        else
        {
            currentAmmo += weaponData.ammoAmount;
            weaponData.ammoAmount = 0;
        }


      

        return true;
    }

}
