using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDummy : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSource;
    public ParticleSystem ps;

    [Header("Audios")]
    public AudioClip dummyHitSound;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("AttackHit"))
            return;
        if (ps) ps.Play();
        anim.SetTrigger("isHit");
        audioSource.PlayOneShot(dummyHitSound);
    }
}
