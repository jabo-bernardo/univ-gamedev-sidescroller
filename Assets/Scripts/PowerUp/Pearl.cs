using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pearl : MonoBehaviour
{

    private BoxCollider2D boxCollider;

    void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (!collider.gameObject.CompareTag("Player")) return;
        GameManager.Instance.SetPlayerHealth(GameManager.Instance.GetPlayerHealth() + GameManager.Instance.GetPlayerMaxHealth() * 0.10f);
        Destroy(gameObject);
    }
}
