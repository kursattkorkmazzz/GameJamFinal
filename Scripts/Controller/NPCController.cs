using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class NPCController : MonoBehaviour
{

    public List<Dialog> dialogs = new();

    [Header("Trigger")]
    public Vector2 triggerArea;
    public Vector2 triggerOffset;


    public bool isAlreadyTalked = false;
    public bool isPlayerIn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isAlreadyTalked = false;
    }

    // Update is called once per frame
    void Update()
    {


        ConversationTrigger();
        if (isAlreadyTalked) isAlreadyTalked = isPlayerIn;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position + new Vector3(triggerOffset.x, triggerOffset.y, 0), triggerArea);
    }



    void ConversationTrigger()
    {
       
        Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);

        Collider2D[] cols = Physics2D.OverlapBoxAll(playerPosition + triggerOffset, triggerArea, 0);
        isPlayerIn = false;
        foreach (Collider2D col in cols)
        {
       
            if (col.gameObject.tag == "Player")
            {
                isPlayerIn = true;

                if (isAlreadyTalked) continue;


                for (int i = 0; i < dialogs.Count; i++)
                {

                    if (dialogs[i].isComplated == false)
                    {

                        isAlreadyTalked = true;
                        DialogManager.Instance.StartDialog(dialogs[i]);

                        return;
                    }

                }
            }
        }




    }
}
