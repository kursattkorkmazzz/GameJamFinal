using UnityEngine;

public class Sentence 
{
  public string sentence { get; set; }
  public bool isPassed { get; set; }

    public Sentence(string sentence)
    {
        this.sentence = sentence;
        isPassed = false;
    }



    public void SetPassed()
    {
        this.isPassed = true;
    }

    
}
