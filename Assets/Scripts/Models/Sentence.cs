using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choice
{
    public string[] Options;
    // Resolution of choice, index of Options should correspond to index of related response
    public Response[] Responses;

    public Choice(string[] options, Response[] responses) {
        this.Options = options;
        this.Responses = responses;
    }
}

[System.Serializable]
public class Response
{
    public string String;
    public bool HasEvent = false;
    public int EventId;

    public Response(string content = "", bool hasEvent = false, int eventId = 0) {
        this.String = content;
        this.HasEvent = hasEvent;
        this.EventId = eventId;
    }
}

[System.Serializable]
public class Sentence {
    public string Content;
    public bool hasChoice = false;
    public Choice SentenceChoice;  
    
    public Sentence(string content) {
        this.Content = content;
    }

    public Sentence(string content, Choice choice) {
        this.Content = content;
        this.hasChoice = true;
        this.SentenceChoice = choice;
    }
}


