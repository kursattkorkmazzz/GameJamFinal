using UnityEngine;

public class QuestGoal
{
    public string description { get; private set; }
    public int currentAmount { get; private set; }
    public int requiredAmount { get; private set; }
    public bool isComplated { get; private set; }

    public delegate void onQuestGoalComplated();
    public event onQuestGoalComplated OnQuestGoalComplated;

    public delegate void onQuestGoalChanged();
    public event onQuestGoalChanged OnQuestGoalChanged;

    public QuestGoal() {
        this.currentAmount = 0;
        this.requiredAmount = 0;
    }

    public QuestGoal SetDescription(string description)
    {
        this.description = description;
        return this;
    }

    public QuestGoal SetRequiredAmount(int requiredAmount)
    {
        if (requiredAmount > 0) this.requiredAmount = requiredAmount;
        return this;
    }

    public QuestGoal ComplateOneStep()
    {
        currentAmount += 1;
        if (currentAmount > requiredAmount) currentAmount = requiredAmount;
        CheckComplated();


        OnQuestGoalChanged?.Invoke();
        return this;
    }


    private QuestGoal CheckComplated()
    {
        isComplated = false;
        if(currentAmount >= requiredAmount)
        {
            OnQuestGoalComplated?.Invoke();
            isComplated = true;
        }
        
        return this;
    }


   

}
