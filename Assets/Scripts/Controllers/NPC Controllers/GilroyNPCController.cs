using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GilroyNPCController : NPCController
{
    public bool HasToldStory = false;
    public SpriteRenderer Sprite;
    private Dialog Story;
    private Dialog StoryPrompt = new Dialog(new Sentence[] { 
        new Sentence("Do you really want to hear all that again?",  new Choice(new string[] { "Yes", "No" },
                    new Response[] { new Response("Alright, if you insist...", true, 6),
                                     new Response("Good. Now if you'll excuse me I should return to my work.", false, 0) })) }, "Gilroy");
    // Start is called before the first frame update


    protected new void Start() {
        base.Start();
        Story = Dialog;
        GameEvents.current.onGilroyStoryEnd += PromptStory;
        GameEvents.current.onGilroyStoryReset += ResetStory;
    }

    private void PromptStory() {
        HasToldStory = true;
        Dialog = StoryPrompt;
    }

    private void ResetStory() {
        DialogController.EndDialog();
        Dialog = Story;
        DialogController.StartDialog(Dialog);
    }

    private void LateUpdate() {
        float posY = Sprite.transform.position.y + Mathf.Sin(Time.time * 2f) * 0.0003f;
        Sprite.transform.position = new Vector3(Sprite.transform.position.x, posY);
    }
}
