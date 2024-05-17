using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCOverheadText : MonoBehaviour
{
    public GameObject titleText;
    public GameObject subtitleText;

    public bool shouldShowTitle = true;
    public bool shouldShowSubtitle = false;

    void Update() {
        if (shouldShowTitle) titleText.SetActive(true);
        else titleText.SetActive(false);
        if (shouldShowSubtitle) subtitleText.SetActive(true);
        else subtitleText.SetActive(false);
    }
}
