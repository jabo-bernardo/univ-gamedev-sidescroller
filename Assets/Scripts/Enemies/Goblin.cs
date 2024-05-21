using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    private DialogueTrigger dialogueTrigger;
    void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }
    void OnBecameVisible() {
        if (!GameManager.Instance.GetIsFirstGoblin()) return;
        StartCoroutine(ShowMyDialogue());
        GameManager.Instance.IHaveSeenGoblin();
    }

    IEnumerator ShowMyDialogue() {
        yield return new WaitForSeconds(0.250f);
        dialogueTrigger.TriggerDialogue();
    }
}
