using System;
using UnityEngine;

public class InstantHealthBoost : BaseBoost<InstantHealthBoostSO>
{

    public InstantHealthBoostSO boostData;
    public override InstantHealthBoostSO BoostData { get => boostData; set=> boostData = value; }

    public override event ICollectible.onCollected OnCollected;

    public override GameObject GetObject()
    {
        return gameObject;
    }

    
}
