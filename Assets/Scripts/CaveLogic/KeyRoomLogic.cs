using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyRoomLogic : MonoBehaviour
{
    public int roomLevel;
    public DialogueTrigger noKeyDialogue;
    public DialogueTrigger hasKeyDialogue;
    public bool hasTriggered;

    void Start() {
        
    }

    void Update() {
        if (hasTriggered) return;
        if (GameManager.Instance.GetCastleKeyLocation() == roomLevel) {
            hasKeyDialogue.TriggerDialogue();
            hasTriggered = true;
        } else {
            noKeyDialogue.TriggerDialogue();
            hasTriggered = true;
        }
    }
}
