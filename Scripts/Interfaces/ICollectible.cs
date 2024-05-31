using UnityEngine;
public interface ICollectible
{
    public GameObject GetObject();


    public delegate void onCollected(GameObject collected);
    public event onCollected OnCollected;
}
