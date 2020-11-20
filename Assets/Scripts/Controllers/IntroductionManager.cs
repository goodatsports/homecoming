using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class IntroductionManager : MonoBehaviour
{
    public GameObject TextboxUI;
    public RectTransform Background;
    public DialogController DialogControl;
    public InputMaster Controls;
    public InputAction DialogAdvance;


    [SerializeField]
    public Dialog IntroDialog;
    void Awake() {
        Controls = new InputMaster();
        DialogAdvance = Controls.Intro.AdvanceDialog;
        DialogAdvance.started += ctx => { AdvanceDialog(); };

        IntroDialog.name = "";
    }

    void OnEnable() {
        Controls.Intro.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        TextboxUI.SetActive(true);
        StartCoroutine(StartIntro());
    }

    IEnumerator StartIntro() {
        yield return new WaitForSeconds(1f);
        DialogControl.StartDialog(IntroDialog);
    }

    void AdvanceDialog() {
        if (DialogControl.HasSentences) {
            DialogControl.DisplayNextSentence();
        }
        else print("IT'S OVER, LOAD NEXT SCENE");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
