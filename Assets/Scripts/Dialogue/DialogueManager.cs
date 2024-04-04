using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    private AudioSource audioSource;

    public TMP_Text sentenceOutput;
    public Canvas dialogueCanvas;
    public Animator dialogueBoxAnimation;

    [Header("Audios")]
    public AudioClip dialogueSound;


    void Start()
    {
        sentences = new Queue<string>();
        audioSource = GetComponent<AudioSource>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        Debug.Log(dialogue.name);
        GameManager.Instance.DisableUserActions();
        dialogueBoxAnimation.SetBool("isOpen", true);
        audioSource.PlayOneShot(dialogueSound);
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
            Debug.Log(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            if (!dialogueBoxAnimation.GetBool("isOpen"))
                return;
            EndDialogue();
            return;
        }
        audioSource.PlayOneShot(dialogueSound);
        string currentSentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        sentenceOutput.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            sentenceOutput.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void EndDialogue()
    {
        dialogueBoxAnimation.SetBool("isOpen", false);
        sentences.Clear();
        GameManager.Instance.EnableUserActions();
        audioSource.PlayOneShot(dialogueSound);
    }
}
