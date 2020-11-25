using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ChoiceController : MonoBehaviour
{
    public SpriteRenderer Pointer;
    public TextMeshProUGUI Choice1, Choice2;
    private Vector3 initialPointerPos, pointerOffset;

    // Currently selected choice
    private int ChoiceSelect = 0;
    // Choice that player confirms with input 
    private int Decision = -1;

    // Response from player's confirmed Choice
    public Response Resolution;
    // Start is called before the first frame update

    private void Awake() 
    {
        pointerOffset = new Vector3 (Choice1.transform.position.x - Pointer.transform.position.x, 0, 0);
    }

    void Start() {

    }

    public IEnumerator Resolve(Choice choice) {
        gameObject.SetActive(true);
        initialPointerPos = Pointer.transform.position;
        Choice1.text = choice.Options[0];
        Choice2.text = choice.Options[1];

        // wait for user to confirm choice
        print("decision: " + Decision);
        yield return new WaitUntil(() => Decision > -1);
        print("DECISION MADE - resolve");

        // Get corresponding Response from Choice and pass as an event message back to DialogController
        Resolution = choice.Responses[Decision];
        GameEvents.current.DialogChoiceMade(Resolution);
        Reset();
    }

    public void Confirm() {
        // while no player Confirm input, wait
        // when Confirm input detected, set Decision field
        Decision = ChoiceSelect;
        print("decision in confirm after assignment: " + Decision);

    }

    public void Left() {
        if (ChoiceSelect > 0) {
            ChoiceSelect -= 1;
            Pointer.transform.position = Choice1.transform.position - pointerOffset;
        }

    }

    public void Right() {
        if (ChoiceSelect < 1) {
            ChoiceSelect += 1;
            Pointer.transform.position = Choice2.transform.position - pointerOffset;
        }

    }

    void Reset() {
        print("resetting choice...");
        Pointer.transform.position = initialPointerPos;
        gameObject.SetActive(false);
        ChoiceSelect = 0;
        Decision = -1;
    }

}
