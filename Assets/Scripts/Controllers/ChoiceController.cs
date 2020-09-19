using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ChoiceController : MonoBehaviour
{
    public SpriteRenderer Pointer;
    public TextMeshProUGUI Choice1, Choice2;
    private float pointerOffset;
    private int ChoiceSelect = 0;
    // Start is called before the first frame update

    private void Awake() 
    {
        
        pointerOffset = Choice1.transform.position.x - Pointer.transform.position.x;
    }

    public void Prompt(Choice choice) {
        print("in choice prompt");
    }
    
}
