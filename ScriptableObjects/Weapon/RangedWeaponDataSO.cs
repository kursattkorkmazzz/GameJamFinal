using UnityEngine;
using System;

[CreateAssetMenu(fileName = " New Ranged Weapon", menuName = "Weapons/Ranged Weapon")]
public class RangedWeaponDataSO : WeaponDataSO
{
    public float bulletSpeed;
    public int maxMagSize;
    public int ammoAmount;
    public GameObject bulletPrefab;
    public Vector3 bulletSpawnPoint;
}
