using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : NPCController
{
    [SerializeField]
    protected override void Awake() {
        minTiming = 0.25f;
        maxTiming = 4f;
        Dialog = new Dialog(new string[] { $"I am {Name}." }, Name);
        base.Awake();
    }

}
