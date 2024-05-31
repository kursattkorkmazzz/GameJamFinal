using UnityEngine;


public abstract class WeaponDataSO : ItemDataSO, IStorable
{
    public string ItemId { get => ID; set => ID = value; }

    public Sprite iconUI;
    public Sprite IconUI { get => iconUI; set => iconUI = value; }


    public float damage;
    public Vector3 weaponHoldPosition;
    public float fireAmountPerSecond;

}
