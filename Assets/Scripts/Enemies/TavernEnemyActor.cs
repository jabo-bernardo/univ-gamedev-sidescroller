using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernEnemyActor : MonoBehaviour
{
    public float movementSpeed = 2.0f;
    private Rigidbody2D rigidBody;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.Instance.GetShouldActorsRunaway()) {
            if (transform.position.y < -1.73) {
                Vector2 targetPos = new Vector2(transform.position.x, -1.72f);
                transform.position = Vector2.Lerp(transform.position, targetPos, 1f);
            }
            rigidBody.velocity = new Vector2(-1 * movementSpeed, rigidBody.velocity.y);
        }
    }
}
