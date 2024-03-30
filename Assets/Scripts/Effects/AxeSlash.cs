using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSlash : MonoBehaviour
{
    public int effectVariations = 3;

    private Animator anim;
    private AudioSource audioSource;

    [Header("Audios")]
    public AudioClip[] slashSounds;

    void Start()
    {
        anim = GetComponent<Animator>();
        int SLASH_TO_USE = Mathf.RoundToInt(Random.Range(0, effectVariations));
        anim.SetTrigger("isSlash");
        anim.SetInteger("SlashIndex", SLASH_TO_USE);

        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(slashSounds[Random.Range(0, slashSounds.Length)]);
    }
}
