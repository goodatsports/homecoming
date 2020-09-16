using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public SpriteRenderer ItemSprite;
    public TextMeshPro Name;
    public TextMeshPro Description;

    private void Awake() {
        
    }

    public void SetSprite(Sprite sprite) {
        ItemSprite.sprite = sprite;
    }

    public void SetName(string name) {
        Name.text = name;
    }

    public void SetDescription(string description) {
        Description.text = description;
    }
}
