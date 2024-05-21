using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavePlatform : MonoBehaviour
{
    private DialogueTrigger dialogueTrigger;

    void Start() {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    void Update() {
        if (GameManager.Instance.GetShouldShowKeys()) {
            gameObject.SetActive(true);
            return;
        }
        gameObject.SetActive(false);
    }

    void OnBecameVisible() {
        if (!GameManager.Instance.GetIsFirstCavePlatform()) return;
        dialogueTrigger.TriggerDialogue();
        GameManager.Instance.IHaveSeenCavePlatform();
    }
}
