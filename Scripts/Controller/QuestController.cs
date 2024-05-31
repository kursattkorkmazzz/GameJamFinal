using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class QuestController : MonoBehaviour
{

    [SerializeField] public Quest currentQuest;
    public List<Quest> questList { get; private set; }


    [Header("UI Related Properties")]
    public GameObject QuestHUD;
    public GameObject QuestListObject;
    public GameObject QuestPanelPrefab;
    public GameObject QuestGoalPrefab;

    public bool isUIOpenned;

    void Start()
    {

        currentQuest = null;

        isUIOpenned = false;
        QuestHUD.SetActive(false);


        UpdateUI();
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.J))
        {
            ToggleUI();
        }
    }




    #region Current Quest CRUD Methods

    private void SetCurrentQuest(Quest newCurrentQuest)
    {
        currentQuest = newCurrentQuest;
    }

    public void GoNextQuest()
    {
        foreach (Quest quest in questList)
        {
            if (!quest.isComplated)
            {
                SetCurrentQuest(quest);
                break;
            }
        }

    }

    #endregion

    #region Quest CRUD Methods
    public void AddQuest(Quest newQuest)
    {

        if (questList == null) questList = new();

        newQuest.OnAnyGoalComplated += UpdateUI;
        newQuest.OnAnyGoalChanged += UpdateUI;
        newQuest.OnQuestComplated += OnQuestComplatedHandler;
        questList.Add(newQuest);
        GoNextQuest();
        UpdateUI();
    }

    public void RemoveQuest(Quest removeQuest)
    {
        questList.Remove(removeQuest);
        UpdateUI();
    }
    public Quest GetQuest(int index)
    {
        if (index >= questList.Count) return null;
        return questList[index];
    }

    public bool IsContains(Quest quest)
    {
        return questList.Contains(quest);
    }

    #endregion

    #region UI Related Methods

    public void UpdateUI()
    {
        DestroyAllQuestsFromUIList();

        if (questList == null) return;

        foreach (Quest quest in questList)
        {
            if (quest == null) continue;

            GameObject questPanel = Instantiate(QuestPanelPrefab, QuestListObject.transform);


            Image questPanelImage = questPanel.GetComponent<Image>();
            TextMeshProUGUI questNameUI = questPanel.transform.Find("Quest Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI questDescriptionUI = questPanel.transform.Find("Quest Description").GetComponent<TextMeshProUGUI>();
            Toggle isComplatedToggleUI = questPanel.transform.Find("Select").GetComponent<Toggle>();
            GameObject questGoalList = questPanel.transform.Find("Quest Goal List").gameObject;


            if (questPanelImage != null && quest == currentQuest)
                if (questPanelImage != null && quest == currentQuest && !quest.isComplated) questPanelImage.color = new Color32(190, 255, 173, 255);
                
             
            if (questNameUI != null) questNameUI.text = quest.questName; // Setting name of quest.
            if (questDescriptionUI != null) questDescriptionUI.text = quest.questDescription + "\n\nReward: " + quest.rewardXP;  // Setting description of quest.

            if (isComplatedToggleUI != null) isComplatedToggleUI.isOn = quest.isComplated;  // Setting the checking complated toggle of quest.

            if (questGoalList != null) // Adding all quest goals.
            {


                List<QuestGoal> goals = quest.GetAllQuestGoals();


                foreach (QuestGoal goal in goals)
                {
                    GameObject questGoalpanel = Instantiate(QuestGoalPrefab, questGoalList.transform);

                    TextMeshProUGUI questGoalDescriptionUI = questGoalpanel.transform.Find("Goal Description").GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI questGoalStatusUI = questGoalpanel.transform.Find("Goal Status").GetComponent<TextMeshProUGUI>();


                    if (questGoalDescriptionUI != null) questGoalDescriptionUI.text = goal.description; // Setting description of goal.
                    if (questGoalStatusUI != null) questGoalStatusUI.text = goal.currentAmount.ToString() + "/" + goal.requiredAmount.ToString(); // Setting status of goal.

                }

            }


        }






    }

    private void DestroyAllQuestsFromUIList()
    {
        foreach (Transform obj in QuestListObject.GetComponentsInChildren<Transform>())
        {
            if (obj.gameObject == QuestListObject) continue;
            Destroy(obj.gameObject);
        }
    }

    public bool ToggleUI()
    {
        QuestHUD.SetActive(isUIOpenned);
        isUIOpenned = !isUIOpenned;
        return !isUIOpenned;
    }
    #endregion


    #region Event Methods

    public void OnQuestComplatedHandler(float reward, Quest quest)
    {

        // Giving the XP reward to Player.
        if (gameObject.TryGetComponent<PlayerManager>(out PlayerManager pm))
        {
            pm.levelStats.GainXP(reward);
        }




        GoNextQuest();
        UpdateUI();
    }


    public void OnQuestGoalChangedHandler()
    {
        UpdateUI();
    }

    #endregion
}
