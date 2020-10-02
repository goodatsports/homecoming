using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choice
{
    public string[] Options;
    public int OptionChoice;
    // Resolution of choice, index of Options should correspond to index of related response
    public Response[] Responses;
}

[System.Serializable]
public class Response
{
    public string String;
    public bool HasEvent = false;
    public int EventId;
}

[System.Serializable]
public class Sentence {
    public string Content;
    public bool hasChoice = false;
    public Choice SentenceChoice;  
    
    public Sentence(string content) {
        this.Content = content;
    }
}


