using UnityEngine;


[System.Serializable]
public class LevelManager
{

    
    [SerializeField]  public int currentLevel { get { return CheckLevel(); } private set { } }
    [SerializeField]  public float currentXP { get; private set; }
    public float[] targetXPList { get; private set; }

    public LevelManager(float[] targetXPList)
    {
        this.targetXPList = targetXPList;
    }

    
    public float GainXP(float xpAmount)
    {
        if(xpAmount > 0)
        {
            currentXP += xpAmount;
        }

        return currentXP;
    }


    public int CheckLevel() {

        for(int i = 0;i < targetXPList.Length; i++){
            if(currentXP < targetXPList[0])
            {
                currentLevel = 0;
                break;
            }
            if (currentXP >= targetXPList[i]) currentLevel = i + 1;
        }


        return currentLevel;
    }

}
