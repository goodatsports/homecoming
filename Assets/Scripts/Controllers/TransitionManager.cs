using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public Canvas canvas;
    public Animator animator;
    private int nextScene;

    // Start is called before the first frame update
    void Start()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene > SceneManager.sceneCountInBuildSettings - 1) nextScene = 0;
        GameEvents.current.onGameEnd += StartTransition;
    }

    public void StartTransition() {
        animator.enabled = true;
    }

    public void SceneChange() {
        SceneManager.LoadScene(nextScene);
    }
}
