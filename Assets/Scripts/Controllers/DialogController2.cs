using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEditor;
using Cinemachine;

public class DialogController2 : MonoBehaviour
{
    public GameObject Textbox;
    public GameObject ChoiceUI;
    public SpriteRenderer Sprite;
    public Image TextboxBackground;
    public TextMeshPro nameText;
    public TextMeshPro dialogText;
    public ChoiceController Choices;
    public Animator animator;
    public AudioClip sound;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    public InputMaster Controls;
    public InputAction CursorMove, CursorConfirm;

    private Queue<Sentence> sentences;
    private bool isTyping = false;
    private Sentence currentSentence;
    private bool _hasSentences = true;
    public bool HasSentences { get => _hasSentences; private set { _hasSentences = value; } }

    // Used to account for camera's distance from game board (10 units) when calculating viewport->worldpoint values
    private float CAMERA_Z_OFFSET = 8f;

    private void Awake() {
        Controls = new InputMaster();
        CursorMove = Controls.Dialog.MoveCursor;
        CursorConfirm = Controls.Dialog.Confirm;
    }

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = "";
        dialogText.text = "";
        Textbox.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.0f, CAMERA_Z_OFFSET));

        Textbox.SetActive(false);
        ChoiceUI.SetActive(false);

        sentences = new Queue<Sentence>();

    }

    void PlaySound()
    {
        source.PlayOneShot(sound);
    }

    public void StartDialog(Dialog dialog)
    {
        HasSentences = true;
        Textbox.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.0f + 0.2f, CAMERA_Z_OFFSET));
        Textbox.SetActive(true);
        // gameObject.AddComponent<AudioSource>();
        //source.clip = sound;
        //source.playOnAwake = false;
        // animator.SetBool("IsOpen", true);
        nameText.text = dialog.name;
        sentences.Clear();

        foreach (Sentence sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0 && !isTyping)
        {
            EndDialog();
            print("in display sentences");
            return;
        }

        // If current sentence is still being typed out, 
        // stop typing and write out all at once 
        if (isTyping) {
            StopAllCoroutines();
            dialogText.text = currentSentence.Content;
            isTyping = false;
        }
        else {
            currentSentence = sentences.Dequeue();

            // Prompt choice if current sentence has one
            if (currentSentence.hasChoice) {
                PromptChoice(currentSentence.SentenceChoice);
            }
            StopAllCoroutines();
            StartCoroutine(TypeSentence(currentSentence.Content));
        }
    }

    public void PromptChoice(Choice sentenceChoice) {
        GameEvents.current.WaitForDialogChoice();
        Choices.Prompt(sentenceChoice);
        
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
           // PlaySound();
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
        }
        isTyping = false;
    }

    public void EndDialog()
    {
        StopAllCoroutines();
        Textbox.SetActive(false);
        HasSentences = false;
        //animator.SetBool("IsOpen", false);
    }

}
