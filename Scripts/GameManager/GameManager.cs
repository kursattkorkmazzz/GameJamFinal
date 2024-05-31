using UnityEngine;

public class GameManager : MonoBehaviour
{

    public NPCController kingNPC;
    public QuestController playerQuestControler;


    private QuestStateManager questStateManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        questStateManager = new QuestStateManager(playerQuestControler,kingNPC);
        questStateManager.Initialization(questStateManager.StarterQuest);


    }

    private void Update()
    {
        
    }

}
