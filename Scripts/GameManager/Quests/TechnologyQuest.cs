using UnityEngine;
using System;
using System.Collections.Generic;
using DesignPatterns.FiniteStateMachine;
public class TechnologyQuest : QuestState
{


    protected Dialog dialog { get; private set; }
    public Quest quest;
    protected Quest nextQuest;
    protected override Dialog Dialog { get => dialog; set { } }


    public TechnologyQuest(QuestStateManager psm,QuestController qc, NPCController npc ) : base(psm,qc,npc){



        quest = new Quest();

        quest.SetQuestName("Rescue Technologies")
            .SetQuestDescription("To kill the boss, Kingdom needs some new health potions.")
            .SetRewardXP(45)
            .AddQuestGoal("Kill 5 enemies at lab.", 5)
            .AddQuestGoal("Collect 3 health potion.", 3);

        quest.OnQuestComplated += OnQuestComplatedHandler;


        dialog = new Dialog();


        dialog.SetSpeaker("King")
            .AddSentence("You were successful! Excellant. Thank you for help. We are not gonna forget you. We are ready for evil and its soldiers.");
            






    }



    public override void Enter()
    {

 

        if (quest != null) playerQuestControler.AddQuest(quest);
        if(dialog != null) kingNPC.dialogs.Add(dialog);





        // Görev tamamlanma şartları
        // 1. Kill five enemy at lab.
        List<GameObject> ghosts = SpawnManager.Instance.SpawnEnemy<Ghost>(SpawnManager.Instance.labEnemySpawnPoints, 5);


        foreach (GameObject ghost in ghosts)
        {
            if (ghost.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.OnEnemyDie += OnEnemyQuestGoalComplateHandler;
            }

        }

        // 2. Collect 3 health potion.
        GameObject.FindGameObjectWithTag("Player").GetComponent<CollectibleController>().OnCollected += OnHealtPotionQuestGoalComplateHandler;
    }

    public override void Exit()
    {
       
    }

    public override void Update()
    {
        
    }

    public void OnQuestComplatedHandler(float reward, Quest quest)
    {
        SpawnManager.Instance.TeleportPlayerToExactly(playerQuestControler.transform.gameObject, SpawnManager.Instance.castlePlayerSpawnPoint);
        Debug.Log("Technology quest complated");
    }

    public void OnEnemyQuestGoalComplateHandler()
    {
        quest.GetQuestGoalAt(0).ComplateOneStep();
    }
    public void OnHealtPotionQuestGoalComplateHandler(GameObject collected)
    {
       if(collected.TryGetComponent<InstantHealthBoost>(out InstantHealthBoost boost))
        {
            quest.GetQuestGoalAt(1).ComplateOneStep();
        }




    }


}
