using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bartender : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;
    public BanditController banditController;

    void Start() {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        banditController = GameObject.Find("BanditsController").GetComponent<BanditController>();
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (GameManager.Instance.GetHasTalkedWithBartender()) return;
        if (collider.gameObject.CompareTag("Player")) {
            if (Input.GetMouseButton(0)) {
                dialogueTrigger.TriggerDialogue(OnConversationEnd);
                GameManager.Instance.SetHasTalkedWithBartender(true);
            }
        }
    }

    void OnConversationEnd() {
        GameManager.Instance.DisableUserActions();
        GameManager.Instance.SetShouldCameraFocusOnActors(true);
        CameraFollow camFollow = GameObject.Find("MainCamera").GetComponent<CameraFollow>();
        camFollow.isCinematic = true;
        camFollow.targetObject = GameObject.Find("CAMPOS_BanditRunaway");
        banditController.HandleBanditCinematic();
    }
}
