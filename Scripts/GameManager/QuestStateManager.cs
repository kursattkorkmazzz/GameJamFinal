using UnityEngine;
using DesignPatterns.FiniteStateMachine;

public class QuestStateManager : FiniteStateMachine
{

    public QuestState StarterQuest;
    public FarmerQuest HelpFarmerQuest;
    public TechnologyQuest TechnologyQuest;


    public delegate void onQuestChanged();
    public event onQuestChanged OnQuestChanged;

    public QuestStateManager(QuestController qc, NPCController npc)
    {
        HelpFarmerQuest = new FarmerQuest(this, qc, npc);
        StarterQuest = new WelcomeQuest(this,qc,npc);
        TechnologyQuest = new TechnologyQuest(this, qc, npc);
    }



    public override void ChangeState(State newState)
    {
        base.ChangeState(newState);
        OnQuestChanged?.Invoke();
        Debug.Log(currentState.ToString());
    }

    public QuestState GetCurrrentState()
    {
        return (QuestState)currentState;
    }
}
