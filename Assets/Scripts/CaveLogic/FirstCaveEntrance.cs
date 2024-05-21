using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCaveEntrance : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;

    void Start()
    {
        
    }

    void Update() {
        if (GameManager.Instance.GetIsFirstCaveEntrance()) {
            dialogueTrigger.TriggerDialogue();
            GameManager.Instance.IHaveSeenCave();
        }   
    }
}
