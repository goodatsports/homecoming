using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    public string name;
    public Sentence[] sentences;

    public Dialog(string[] sentences, string name) {

        Sentence[] dialogContent = new Sentence[sentences.Length];
        int index = 0;
        foreach (string sentence in sentences) {
            dialogContent[index] = new Sentence(sentence);
            index++;
        }
        this.sentences = dialogContent;
        this.name = name;
    }

    public Dialog(Sentence[] sentences, string name) {
        this.name = name;
        this.sentences = sentences;
    }

}
