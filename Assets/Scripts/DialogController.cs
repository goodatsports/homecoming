using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogController : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    private Queue<Sentence> sentences;
    public Animator animator;
    public AudioClip sound;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }
    private bool _hasSentences = true;
    public bool HasSentences { get => _hasSentences; private set { _hasSentences = value; }  }

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<Sentence>();
    }

    void PlaySound() {
        source.PlayOneShot(sound);
    }

    public void StartDialog(Dialog dialog) 
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
        animator.SetBool("IsOpen", true);
        nameText.text = dialog.name;
        sentences.Clear();

        foreach (Sentence sen in dialog.sentences) 
        {
            sentences.Enqueue(sen);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            HasSentences = false;
            EndDialog();
            return;
        }

        Sentence sentence = sentences.Dequeue();
        if (sentence.hasChoice) print("CHOICE HERE!");
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence.Content));
    }

    IEnumerator TypeSentence (string sentence) {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogText.text += letter;
            PlaySound();
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
        }
    }

    void EndDialog() {
        StopAllCoroutines();
        animator.SetBool("IsOpen", false);
    }

}
