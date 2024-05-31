using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BoostManager : MonoBehaviour
{

    BoostStorage boostStorage;

    public GameObject BoostStorageObject;
    public TextMeshProUGUI EmptyTextUIObject;
    public GameObject BoostSlotUIPrefab;

    public BoostDataSO[] boosts;

    public bool isBoostStorageOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isBoostStorageOpen = false;
        RemoveOldSlots();
        boostStorage = new BoostStorage(6); // with 6 capacity.

        foreach(BoostDataSO boostData in boosts)
        {
            if (boostData.Amount > 0)
            {
                boostStorage.storage.Add(boostData);
            }
        }

        UpdateBoostUI();

    }


    private void Update()
    {
        SetVisibilityHUD();
    }

    #region CRUD Boost

    public void AddBoost(BoostDataSO boost)
    {

        if (boostStorage.AddItem(boost)) {

        UpdateBoostUI();
        };
    }

    public void UseBoost(BoostDataSO usedBoost)
    {
      
            usedBoost.Use(gameObject);
            int deletedCount = boostStorage.RemoveItem(usedBoost.ItemId, 1);

            UpdateBoostUI();
    }




    #endregion

    #region Utility Methods




    private void RemoveOldSlots()
    {

    
        foreach (Transform oldSlot in BoostStorageObject.GetComponentInChildren<Transform>())
        {
            if (oldSlot.gameObject == BoostStorageObject) continue;

            if (oldSlot.gameObject.name == "EmptyMessage") continue;
            
            Destroy(oldSlot.gameObject);
        }
    }

   
    #endregion

    #region UI related methods

    private void SetVisibilityHUD()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {
            isBoostStorageOpen = !isBoostStorageOpen;
        }

        BoostStorageObject.SetActive(isBoostStorageOpen);
        
    }


    private void UpdateBoostUI()
    {

        RemoveOldSlots();

        if(boostStorage.Count() <= 0){
            EmptyTextUIObject.gameObject.SetActive(true);
            return;
        }
        else
        {
            EmptyTextUIObject.gameObject.SetActive(false);
        }

        foreach (BoostDataSO boost in boostStorage.storage)
        {



            GameObject newSlot = Instantiate(BoostSlotUIPrefab, BoostStorageObject.transform);
            TextMeshProUGUI amount = newSlot.transform.Find("ItemAmount")?.GetComponent<TextMeshProUGUI>();
            Image icon = newSlot.transform.Find("ItemIcon")?.GetComponent<Image>();
            Button button = newSlot.transform.Find("ClickEvent")?.GetComponent<Button>();

            if (amount != null)
            {
                amount.text = boost.Amount.ToString();
            }
            if (icon != null)
            {
                icon.sprite = boost.IconUI;
            }
            if (button != null)
            {
                button.onClick.AddListener(() =>
                {
                    UseBoost(boost);
                });
            }
        }
    }

    
    #endregion


}
