using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beer : MonoBehaviour
{
    private DialogueTrigger dialogueTrigger;

    void Start() {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (!collider.gameObject.CompareTag("Player")) return;
        GameManager.Instance.IncreaseBeerCount();
        Destroy(gameObject);
        if (GameManager.Instance.yoloTracker.ContainsKey(gameObject.name))
            return;
        GameManager.Instance.yoloTracker.Add(gameObject.name, true);
    }

    void OnBecameVisible() {
        if (!GameManager.Instance.GetIsFirstBeer()) return;
        dialogueTrigger.TriggerDialogue();
        GameManager.Instance.IHaveSeenBeer();
    }
}
