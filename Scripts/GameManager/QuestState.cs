using System;
using DesignPatterns.FiniteStateMachine;
public abstract class QuestState : State
{

    protected abstract Dialog Dialog { get;  set; }
    protected NPCController kingNPC;
    protected QuestController playerQuestControler;
    protected QuestStateManager questStateManager;
    public QuestState(QuestStateManager qsm,QuestController qc,NPCController npc)
    {
        this.playerQuestControler = qc;
        this.kingNPC = npc;
        this.questStateManager = qsm;
    }


}
