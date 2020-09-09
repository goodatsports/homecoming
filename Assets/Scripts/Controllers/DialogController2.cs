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

    public TextMeshProUGUI nameText;
    public TextMeshPro dialogText;
    private Queue<string> sentences;
    public Animator animator;
    public AudioClip sound;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    // Start is called before the first frame update
    void Start()
    {
      
        dialogText.transform.position = Camera.main.ViewportToWorldPoint(new Vector3 (0.5f,0.2f,8f));

        // Document this somewhere else, 
        // numbers come from the pixels-per-unit on the sprites - 32px per unit, so half height is that ratio multiplied by 2
        dialogText.rectTransform.sizeDelta = new Vector2(Screen.width / 32, Screen.height / 64);
        sentences = new Queue<string>();
    }

    private void Update()
    {
        dialogText.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, 8f));
    }

    void PlaySound()
    {
        source.PlayOneShot(sound);
    }

    public void StartDialog(Dialog dialog)
    {
        CameraState conversation = new CameraState();

        Camera.main.transform.position = GameObject.Find("Player").GetComponent<Transform>().position;
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
       // animator.SetBool("IsOpen", true);
        //nameText.text = dialog.name;
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

    void EndDialog()
    {
        StopAllCoroutines();
        //animator.SetBool("IsOpen", false);
    }

}
