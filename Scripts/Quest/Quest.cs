using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class Quest
{


    public string questName { get;private set; }

    public string questDescription { get; private set; }
    public float rewardXP { get; private set; }
    public bool isComplated { get; private set; }
    public List<QuestGoal> goals { get; private set; }


    #region Observer Pattern/Event Properties
    public delegate void onQuestComplated(float reward, Quest quest);
    public event onQuestComplated OnQuestComplated;

    public delegate void onAnyGoalComplated();
    public event onAnyGoalComplated OnAnyGoalComplated;

    public delegate void onAnyGoalChanged();
    public event onAnyGoalChanged OnAnyGoalChanged;

    #endregion


    public Quest()
    {
        this.goals = new();
        this.rewardXP = 0;
        this.isComplated = false;
        this.questName = "No Named Quest";
        this.questDescription = "No description provided.";
    }


    #region Builder Pattern Methods


    public Quest SetRewardXP(float rewardXP)
    {
        if (rewardXP > 0) this.rewardXP = rewardXP;
        return this;
    }

    public Quest SetQuestName(string name)
    {
        if (!name.Equals(" ") && name.Length > 0) this.questName = name;
        return this;
    }
    public Quest SetQuestDescription(string description)
    {
        if (!description.Equals(" ") && description.Length > 0) this.questDescription = description;
        return this;
    }

    #endregion

    #region QuestGoal CRUD Operations
    public Quest AddQuestGoal(string goalDescription, int requiredAmount)
    {
        QuestGoal questGoal = new QuestGoal();
        questGoal.SetDescription(goalDescription).SetRequiredAmount(requiredAmount);
        return AddQuestGoal(questGoal);
    }

    public Quest AddQuestGoal(QuestGoal questGoal)
    {
        questGoal.OnQuestGoalComplated += OnAnyGoalComplatedHandler;
        questGoal.OnQuestGoalChanged += OnAnyGoalChangedHandler;
        goals.Add(questGoal);
        return this;
    }


    public List<QuestGoal> GetAllQuestGoals()
    {
        return goals;
    }

    public QuestGoal GetQuestGoalAt(int index)
    {
        if (index >= goals.Count) return null;
        return goals[index];
    }
    #endregion


    #region Utilities
    public bool CheckQuestComplated()
    {
        foreach (QuestGoal qg in goals)
        {

            
            if (qg.isComplated == false)
            {
                isComplated = false;
                return isComplated;
            }
        }

        isComplated = true;

        OnQuestComplated?.Invoke(rewardXP,this);

        return isComplated;
    }

    #endregion

    #region Event methods

    public void OnAnyGoalComplatedHandler()
    {
        OnAnyGoalComplated?.Invoke();
        CheckQuestComplated();
    }


    public void OnAnyGoalChangedHandler()
    {
        OnAnyGoalChanged?.Invoke();
        CheckQuestComplated();
    }
    #endregion


}
