using UnityEngine;
public abstract class BaseBoost<T>: MonoBehaviour, ICollectible  where T : BoostDataSO
{
    public abstract T BoostData { get; set; }

    public abstract event ICollectible.onCollected OnCollected;

    public abstract GameObject GetObject();


}
