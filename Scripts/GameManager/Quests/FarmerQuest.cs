using UnityEngine;
using System;
using System.Collections.Generic;
using DesignPatterns.FiniteStateMachine;
public class FarmerQuest : QuestState
{


    protected Dialog dialog { get; private set; }
    public Quest quest;
    protected Quest nextQuest;
    protected override Dialog Dialog { get => dialog; set { } }


    public FarmerQuest(QuestStateManager psm,QuestController qc, NPCController npc ) : base(psm,qc,npc){



        quest = new Quest();

        quest.SetQuestName("Help to Farmer")
            .SetQuestDescription("You need to defend all enemies at farm so farmers can provide food resources.")
            .SetRewardXP(30)
            .AddQuestGoal("Kill 3 enemies at farm", 3);

        quest.OnQuestComplated += OnQuestComplatedHandler;





        dialog = new Dialog();


        dialog.AddSentence("You did it... We now have food sources !!!")
            .AddSentence("The next is that if you want to crush their heads, we need to have extra health... ")
            .AddSentence("We produce health solutions at out tech lab but but guess what...")
            .AddSentence("The soldiers of evils also took under control our technology lab.")
            .AddSentence("Just get back it from them. We have some already produced 3 health potion. Take them and bring to castle.")
            .AddSentence("Good luck again...");
        dialog.OnDialogComplated += AttachQuestHandler;






    }

    

    public override void Enter()
    {

 

        if (quest != null) playerQuestControler.AddQuest(quest);
        if(dialog != null) kingNPC.dialogs.Add(dialog);

  

        // Görev tamamlanma şartları

        // Kill one enemy at farm.
        List<GameObject> ghosts = SpawnManager.Instance.SpawnEnemy<Ghost>(SpawnManager.Instance.farmEnemySpawnPoints, 3);

       
        foreach (GameObject ghost in ghosts)
        {
            if (ghost.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.OnEnemyDie += OnQuestComplateHandler;
            }

        }

    }

    public override void Exit()
    {
        dialog.OnDialogComplated -= AttachQuestHandler;
    }

    public override void Update()
    {
        
    }

    public void OnQuestComplatedHandler(float reward, Quest quest)
    {
        SpawnManager.Instance.TeleportPlayerToExactly(playerQuestControler.transform.gameObject, SpawnManager.Instance.castlePlayerSpawnPoint);
        

    }

    public void AttachQuestHandler()
    {
        questStateManager.ChangeState(questStateManager.TechnologyQuest);
        SpawnManager.Instance.TeleportPlayerToExactly(playerQuestControler.transform.gameObject, SpawnManager.Instance.labPlayerSpawnPoint);
    }

    public void OnQuestComplateHandler()
    {
        quest.GetQuestGoalAt(0).ComplateOneStep();
    }



}
