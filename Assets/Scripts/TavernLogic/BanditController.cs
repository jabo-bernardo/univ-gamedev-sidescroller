using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditController : MonoBehaviour
{
    private DialogueTrigger dialogueTrigger;
    public AudioClip battleMusic;

    void Start() {
        if (GameManager.Instance.GetHasTalkedWithBartender()) {
            AudioSource audioSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();
            audioSource.clip = battleMusic;
            audioSource.Play();
        }
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    public void HandleBanditCinematic() {
        AudioSource audioSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();
        audioSource.clip = battleMusic;
        audioSource.Play();
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
        GameManager.Instance.DisableUserActions();
        yield return new WaitForSeconds(1);
        GameManager.Instance.SetShouldActorsRunaway(true);
        yield return new WaitForSeconds(3);
        GameManager.Instance.SetShouldCameraFocusOnActors(false);
        CameraFollow camFollow = GameObject.Find("MainCamera").GetComponent<CameraFollow>();
        camFollow.isCinematic = false;
        camFollow.targetObject = GameObject.Find("Player");
        GameManager.Instance.EnableUserActions();
    }
}
