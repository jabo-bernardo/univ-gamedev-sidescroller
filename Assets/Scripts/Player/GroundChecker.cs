using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Animator anim;

    void OnTriggerEnter2D(Collider2D collider) {
        if (!collider.CompareTag("Ground"))
            return;
        playerMovement.currentJumpCount = 0;
        anim.SetBool("isFalling", false);
    }
}
