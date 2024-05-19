using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernEnemyActor : MonoBehaviour
{
    public float movementSpeed = 2.0f;
    public bool shouldIFlipAfter;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (GameManager.Instance.GetHasTalkedWithBartender())
            Destroy(gameObject);
    }

    void Update()
    {
        if (GameManager.Instance.GetShouldActorsRunaway()) {
            if (shouldIFlipAfter) {
                spriteRenderer.flipX = false;
            }

            if (transform.position.y < -1.73) {
                Vector2 targetPos = new Vector2(transform.position.x, -3f);
                transform.position = Vector2.Lerp(transform.position, targetPos, 1f);
            }
            rigidBody.velocity = new Vector2(-1 * movementSpeed, rigidBody.velocity.y);
            animator.SetBool("isRunning", true);
        }
    }
}
