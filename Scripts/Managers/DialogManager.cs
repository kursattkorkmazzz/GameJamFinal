using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class DialogManager : Singleton<DialogManager>
{



    [Header("UI Related Objects")]
    public GameObject DialogHUDObject;
    public bool isHUDShow;

    private TextMeshProUGUI speakerTextObject;
    private TextMeshProUGUI dialogTextObject;
    private Button buttonObject;
    private TextMeshProUGUI buttonTextObject;


    [Header("Typing Effect")]
    public float typingSpeedInMs;
    IEnumerator typingEffect;


    private Dialog currentDialog;


    #region Event properties
    public delegate void onQuestAttached(List<Quest> quests);
    public event onQuestAttached OnQuestAttached;

    public delegate void onDialogStarted();
    public event onDialogStarted OnDialogStarted;

    public delegate void onDialogComplated();
    public event onDialogComplated OnDialogComplated;
    #endregion

    private void Awake()
    {
        base.InitializeSingletonAwake();
    }

    void Start()
    {
        
        speakerTextObject = DialogHUDObject.transform.Find("Speaker Name").GetComponent<TextMeshProUGUI>();
        dialogTextObject = DialogHUDObject.transform.Find("Dialog").GetComponent<TextMeshProUGUI>();
        buttonObject = DialogHUDObject.transform.Find("Button").GetComponent<Button>();
        buttonTextObject = buttonObject.transform.Find("Text").GetComponent<TextMeshProUGUI>();

        currentDialog = null;


        if(buttonObject != null) buttonObject.gameObject.SetActive(false);
        if (buttonObject != null) buttonObject.onClick.AddListener(NextSentence);

    }


    public void StartDialog(Dialog dialog)
    {


        typingEffect = null;
        StopAllCoroutines();

        if(buttonObject != null) buttonObject.onClick.AddListener(NextSentence);

        if (dialog == null) return;
        currentDialog = dialog;
        if (speakerTextObject != null) speakerTextObject.text = dialog.speaker;

        OpenUI();
        OnDialogStarted?.Invoke();
        NextSentence();

    }

    public void StopDialog()
    {
        typingEffect = null;
        StopAllCoroutines();


        CloseUI();
        currentDialog.CheckComplated();
        currentDialog = null;
        if (buttonObject != null) buttonObject.onClick.RemoveAllListeners();

    }

    public void NextSentence()
    {
        if (currentDialog == null) return;
        
        bool nextStatus = currentDialog.MoveNextSentence();

        Sentence currentSentence = null;
        if (nextStatus)
        {
            currentSentence = currentDialog.GetCurrentSentence();
            
        }
        

        // Clsoe button first
        if (buttonObject != null)
        {
            buttonObject.gameObject.SetActive(false);

            if (currentDialog.IsLastSentence(currentSentence))
            {
                if(OnDialogComplated != null) buttonObject.onClick.AddListener(OnDialogComplated.Invoke);
                buttonTextObject.text = "Finish";
            }
            else
            {
                buttonTextObject.text = "Continue...";
            }

        }
   

        if (dialogTextObject != null)
        {
            dialogTextObject.text = "";
        }


        if (nextStatus)
        {
            typingEffect = TypingEffect(currentSentence, 0);
            StartCoroutine(typingEffect);
        }
        else
        {
            // dialog is finished
            if(currentDialog.quests != null && currentDialog.quests.Count > 0)
            {
                OnQuestAttached?.Invoke(currentDialog.quests);
             
            }

            StopDialog();
         
        }



    }



    #region UI related methods

    private void OpenUI()
    {
        isHUDShow = true;
        DialogHUDObject.SetActive(isHUDShow);
    }
    private void CloseUI()
    {
        isHUDShow = false;
        DialogHUDObject.SetActive(isHUDShow);
    }
    #endregion


    #region Typing Effect
    IEnumerator TypingEffect(Sentence sentence, int index)
    {
        if (index >= sentence.sentence.Length)
        {
            // Show Next Button Object.
            if (buttonObject != null)
            {
                buttonObject.gameObject.SetActive(true);
            }

            sentence.SetPassed();
            
            yield break;

        }

        dialogTextObject.text += sentence.sentence[index];

        yield return new WaitForSeconds(typingSpeedInMs / 1000.0f);
        StartCoroutine(TypingEffect(sentence,++index));
        yield break;
    }
    #endregion
}
