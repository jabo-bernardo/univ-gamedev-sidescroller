using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleKey : MonoBehaviour
{
    public int keyLevel;

    void Start()
    {
        if (GameManager.Instance.GetCastleKeyLocation() == keyLevel && !GameManager.Instance.GetHasCastleKey()) {
            gameObject.SetActive(true);
            return;
        }
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (!collider.gameObject.CompareTag("Player"))
            return;
        GameManager.Instance.SetHasCastleKey(true);
        gameObject.SetActive(false);
    }
}
