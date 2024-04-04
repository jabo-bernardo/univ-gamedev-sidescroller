using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTutorialDialogue());
    }

    IEnumerator StartTutorialDialogue()
    {
        yield return new WaitForSeconds(1);
        GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}
