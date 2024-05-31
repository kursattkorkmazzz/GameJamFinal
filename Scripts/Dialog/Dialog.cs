using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Dialog
{
    public  List<Sentence> sentences;
    private int currentSentence;

    public bool isComplated { get; private set; }
    public List<Quest> quests { get; private set; }
    public string speaker { get; private set; }

    # region Event properties
    public delegate void onDialogComplated();
    public event onDialogComplated OnDialogComplated;
    #endregion

    public Dialog()
    {
        quests = new List<Quest> { };
        isComplated = false;
        sentences = new();
        ResetSentenceSequence();
    }

    #region Dialog Methods

    public Dialog AddQuest(Quest quest)
    {

        if (quest != null) this.quests.Add(quest);
        
        return this;
    }

    public Dialog SetSpeaker(string name)
    {
        if (name.Length > 0 && !name.Equals(" ")) this.speaker = name;

        return this;
    }


    public void CheckComplated()
    {
        isComplated = true;
        foreach (Sentence sentence in sentences)
        {
            if (sentence.isPassed == false)
            {
                isComplated = false;
                return;
            }
        }

        OnDialogComplated?.Invoke();


    }
    #endregion

    #region Sentence CRUD Methods


    public Dialog AddSentence(string sentence)
    {
        if (sentence.Length <= 0 || sentence.Equals(" ")) return this;
        sentences.Add(new Sentence(sentence));
        return this;
    }
    public void ResetSentenceSequence()
    {
        currentSentence = -1;
    }
    public bool MoveNextSentence()
    {
        currentSentence++;
        if (currentSentence >= sentences.Count) return false;
        return true;
    }
    public Sentence GetCurrentSentence()
    {
        
        return sentences[currentSentence];
    }
    public bool IsLastSentence(Sentence sentence)
    {
        return sentence == sentences[sentences.Count - 1];
    }

    #endregion



}
