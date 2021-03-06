using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionManager : MonoBehaviour
{
    public GameObject TextboxUI;
    public GameObject AdvanceIndicator;
    public RectTransform Background;
    public DialogController DialogControl;
    public InputMaster Controls;
    public InputAction DialogAdvance;

    private int nextScene;
    
    [SerializeField]
    public Dialog IntroDialog;
    void Awake() {
        Controls = new InputMaster();
        AdvanceIndicator.SetActive(false);
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene > SceneManager.sceneCountInBuildSettings - 1) nextScene = 0;
        

        //Event subscription for end of intro dialog
        GameEvents.current.onNPCDialogEnd += StartGame;

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

    void StartGame() {
        //GameEvents.current.onNPCDialogEnd -= StartGame;
        SceneManager.LoadScene(nextScene);
    }

    IEnumerator StartIntro() {
        yield return new WaitForSeconds(0.5f);
        DialogControl.StartDialog(IntroDialog);
    }

    void AdvanceDialog() {
        if (DialogControl.HasSentences) {
            DialogControl.DisplayNextSentence();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!DialogControl.isTyping && !AdvanceIndicator.activeSelf) {
            AdvanceIndicator.SetActive(true);
        }
        if (DialogControl.isTyping && AdvanceIndicator.activeSelf) {
            AdvanceIndicator.SetActive(false);
        }
    }
}
