using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditController : MonoBehaviour
{
    private DialogueTrigger dialogueTrigger;

    void Start() {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    public void HandleBanditCinematic() {
        StartCoroutine(Cinematic());
    }

    public void HandleDialogueCallback() {
        StartCoroutine(HandleRun());
    }

    IEnumerator Cinematic() {
        yield return new WaitForSeconds(2);
        dialogueTrigger.TriggerDialogue(HandleDialogueCallback);
        yield return new WaitForSeconds(2);
    }

    IEnumerator HandleRun() {
        yield return new WaitForSeconds(1);
        GameManager.Instance.SetShouldActorsRunaway(true);
        yield return new WaitForSeconds(2);
        GameManager.Instance.SetShouldCameraFocusOnActors(false);
        CameraFollow camFollow = GameObject.Find("MainCamera").GetComponent<CameraFollow>();
        camFollow.isCinematic = false;
        camFollow.targetObject = GameObject.Find("Player");
    }
}
