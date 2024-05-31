using UnityEngine;

public class Pistol : RangedWeapon, ICollectible
{
    public event ICollectible.onCollected OnCollected;

    public GameObject GetObject()
    {
        return gameObject;
    }



  
}
