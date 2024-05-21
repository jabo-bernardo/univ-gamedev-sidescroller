using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleLogic : MonoBehaviour
{
    public GameObject[] enemiesToKill;
    public GameObject portalDoor;
    public SpriteRenderer castleDoor;
    public Sprite castleDoorOpened;
    private DialogueTrigger dialogueTrigger;

    void Start() {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    void Update()
    {
        if (GameManager.Instance.GetIsFirstCastleEntrance()) {
            if (dialogueTrigger) {
                dialogueTrigger.TriggerDialogue();
                GameManager.Instance.IHaveSeenCastle();
            }
        }

        bool IS_STAGE_BEATEN = true;
        Debug.Log(enemiesToKill);
        foreach (GameObject enemy in enemiesToKill) {
            if (enemy) IS_STAGE_BEATEN = false;
        }
        if (!IS_STAGE_BEATEN) {
            portalDoor.SetActive(false);
            return;
        }
        castleDoor.sprite = castleDoorOpened;
        portalDoor.SetActive(true);
    }
}

