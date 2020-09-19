using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choice
{
    public string[] Options;
    public int OptionChoice;
}

// class for Response? 
// 

[System.Serializable]
public class Sentence {
    public string Content;
    public bool hasChoice;
    public Choice SentenceChoice;  
    
    public Sentence(string content) {
        this.Content = content;
    }
}


