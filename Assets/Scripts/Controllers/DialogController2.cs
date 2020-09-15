using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEditor;
using Cinemachine;

public class DialogController2 : MonoBehaviour
{
    public GameObject Textbox;
    public SpriteRenderer Sprite;
    public Image TextboxBackground;
    public TextMeshPro nameText;
    public TextMeshPro dialogText;
    private Queue<string> sentences;
    public Animator animator;
    public AudioClip sound;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }
    private float TextboxHeight;
    private bool _hasSentences = true;
    public bool HasSentences { get => _hasSentences; private set { _hasSentences = value; } }

    // Used to account for camera's distance from game board (10 units) when calculating viewport->worldpoint values
    private float CAMERA_Z_OFFSET = 8f;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = "";
        dialogText.text = "";
        TextboxHeight = TextboxBackground.GetComponent<RectTransform>().rect.height;
        Textbox.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.0f, CAMERA_Z_OFFSET));
        Textbox.SetActive(false);
        sentences = new Queue<string>();

    }

    private void LateUpdate()
    {
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

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            print("in display sentences");
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
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
    }

    public void EndDialog()
    {
        StopAllCoroutines();
        Textbox.SetActive(false);
        HasSentences = false;
        //animator.SetBool("IsOpen", false);
    }

}
