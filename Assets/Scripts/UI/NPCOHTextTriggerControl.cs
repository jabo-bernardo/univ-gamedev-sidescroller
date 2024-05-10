using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCOHTextTriggerControl : MonoBehaviour
{
    public string[] whoCanTrigger = { "Player" };
    public NPCOverheadText nPCOverheadTextReference;
    public bool showTitleOnTrigger, showSubtitleOnTrigger;

    void OnTriggerEnter2D(Collider2D collider) {
        foreach (string trigger in whoCanTrigger) {
            if (collider.gameObject.CompareTag(trigger)) {
                if (showSubtitleOnTrigger)
                    nPCOverheadTextReference.shouldShowSubtitle = true;
                if (showTitleOnTrigger)
                    nPCOverheadTextReference.shouldShowTitle = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        foreach (string trigger in whoCanTrigger) {
            if (collider.gameObject.CompareTag(trigger)) {
                if (showTitleOnTrigger)
                    nPCOverheadTextReference.shouldShowTitle = false;
                if (showSubtitleOnTrigger)
                    nPCOverheadTextReference.shouldShowSubtitle = false;
            }
        }
    }
}
