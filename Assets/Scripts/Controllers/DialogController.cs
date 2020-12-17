using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using UnityEditor;
using Cinemachine;
using System.Runtime.InteropServices.ComTypes;

public class DialogController : MonoBehaviour
{
    public Scene MainScene;
    public GameObject Textbox;
    public GameObject ChoiceUI;
    public SpriteRenderer Sprite;
    public Image TextboxBackground;
    public TextMeshPro nameText;
    public TextMeshPro dialogText;
    public ChoiceController Choices;
    public Animator animator;
    public AudioSource SFX;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    public InputMaster Controls;
    public InputAction CursorMove, CursorConfirm;

    private Queue<Sentence> sentences;
    private Dialog WorkingDialog;
    private bool isTyping = false;
    private Sentence currentSentence;
    private bool _hasSentences = true;
    public bool HasSentences { get => _hasSentences; private set { _hasSentences = value; } }

    // Used to account for camera's distance from game board (10 units) when calculating viewport->worldpoint values
    private float CAMERA_Z_OFFSET = 8f;

    private float TYPING_SPEED = 0.025f;

    private void Awake() {
        Controls = new InputMaster();
        CursorMove = Controls.Dialog.MoveCursor;
        CursorConfirm = Controls.Dialog.Confirm;

        CursorMove.started += ctx => { MoveCursor(ctx.ReadValue<float>()); };
        CursorConfirm.started += ctx => { Choices.Confirm(); };

        
        GameEvents.current.onDialogChoiceMade += ChoiceMade;
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
        if (!SFX.isPlaying) {
            SFX.Play();
        }
    }

    // Used to play one-shot dialog without interrupting current Dialog structure
    public IEnumerator AddDialog(Dialog newDialog) {
        Queue<Sentence> currentDialog = sentences;
        WorkingDialog = newDialog;

        StartDialog(newDialog);
        yield return new WaitUntil(() => HasSentences == false);
        //sentences = currentDialog;

    }

    public void StartDialog(Dialog dialog)
    {
        WorkingDialog = dialog;
        HasSentences = true;
        Textbox.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.2f, CAMERA_Z_OFFSET));
        Textbox.SetActive(true);
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
            return;
        }

        StopAllCoroutines();

        // If current sentence is still being typed out, 
        // stop typing and write out all at once 
        if (isTyping) {
            dialogText.text = currentSentence.Content;
            isTyping = false;
        }
        else {
            currentSentence = sentences.Dequeue();
            // Prompt choice if current sentence has one
            if (currentSentence.hasChoice) {
                PromptChoice(currentSentence.SentenceChoice);
            }
            StartCoroutine(TypeSentence(currentSentence.Content));
        }
    }

    public void PromptChoice(Choice sentenceChoice) {
        Controls.Dialog.Enable();
        GameEvents.current.WaitForDialogChoice();
        StartCoroutine(Choices.Resolve(sentenceChoice));
    }

    public void ChoiceMade(Response resolution) {
        StopAllCoroutines();
        currentSentence = new Sentence(resolution.String);

        StartCoroutine(TypeSentence(currentSentence.Content));
        Controls.Dialog.Disable();

        if (resolution.HasEvent) {
            ResolveResponseEvent(resolution);
        }
    }

    void ResolveResponseEvent(Response response) {
        //EndDialog();
        GameEvents.current.DialogChoiceEvent(response.EventId);
    }

    void MoveCursor(float input) {
        if (input > 0) Choices.Right();
        else Choices.Left();
    }


    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            if (!char.IsWhiteSpace(letter)) PlaySound();
            yield return new WaitForSeconds(TYPING_SPEED);
        }
        isTyping = false;
    }

    public void EndDialog()
    {
        StopAllCoroutines();
        Textbox.SetActive(false);
        HasSentences = false;
        GameEvents.current.NPCDialogEnd();
    }

}
