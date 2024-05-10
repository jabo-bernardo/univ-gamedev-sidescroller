using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private Player player;
    private Animator animator;

    void Start() {
        player = GetComponentInParent<Player>();
        animator = GetComponentInParent<Animator>();
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (!collider.CompareTag("Ground"))
            return;
        Debug.Log("Resetting Jump due to: " + collider.gameObject.name);
        player.ResetJumpCount();
        animator.SetBool("isFalling", false);
    }
}
