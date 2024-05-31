using UnityEngine;
using System;
using DesignPatterns.FiniteStateMachine;
public class WelcomeQuest : QuestState
{

   

    protected Dialog dialog { get; private set; }
    protected Quest quest;
    protected override Dialog Dialog { get => dialog; set { } }


    public WelcomeQuest(QuestStateManager qsm,QuestController qc, NPCController npc ) : base(qsm,qc,npc){



        quest = new Quest();


        quest.SetQuestName("Welcome to Dream Kingdom")
            .SetQuestDescription("Welcome the Kingdom of Dream. The king of kindom needs help you. Talk with him.")
            .SetRewardXP(10)
            .AddQuestGoal("Talk with the king.", 1);






        dialog = new Dialog();
        dialog.SetSpeaker("King")
            .AddSentence("Welcome the Dream Kingdom...");
            /*.AddSentence("Thank you for accepting help of us.")
            .AddSentence("The best evil of world will attack us but we are not ready for that and...")
            .AddSentence("I tell you that we most probably die!!!")
            .AddSentence("But... With your help, we have one chance.")
            .AddSentence("Let's start.")
            .AddSentence("To be successfull the defend attak, your soldier must be powerful. The way to do this is through their stomachs but... ")
            .AddSentence("Soldier's of evil attacked to our farm and farmers. Our farms are under control of them. You must take them back so we can provide food resource to our soldiers.")
            .AddSentence("Good luck... You will need.");*/
        dialog.OnDialogComplated += StartMission;


    }



    public override void Enter()
    {

        
        if (quest != null) playerQuestControler.AddQuest(quest);
        if(dialog != null) kingNPC.dialogs.Add(dialog);

        
    }

    private void StartMission() {
        quest.GetQuestGoalAt(0).ComplateOneStep();
        SpawnManager.Instance.TeleportPlayerToExactly(playerQuestControler.transform.gameObject, SpawnManager.Instance.farmPlayerSpawnPoint);
        questStateManager.ChangeState(questStateManager.HelpFarmerQuest);
    }




    public override void Exit()
    {
        dialog.OnDialogComplated -= StartMission;
    }


    public override void Update()
    {

    }



}
