using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choice
{
    public string[] Options;
}


[System.Serializable]
public class Sentence {
    public string Content;
    public Choice SentenceChoice;    
}


