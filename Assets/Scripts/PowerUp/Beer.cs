using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider) {
        if (!collider.gameObject.CompareTag("Player")) return;
        GameManager.Instance.IncreaseBeerCount();
        Destroy(gameObject);
    }
}
