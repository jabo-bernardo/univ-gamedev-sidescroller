using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (SpriteRenderer))]
[RequireComponent(typeof (BoxCollider2D))]
public class PuzzleKey : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private string keyName;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        keyName = gameObject.name;
    }
    
    void Update() {
        bool shouldForceHide = false;

        if (GameManager.Instance.GetHasFirstKey() && keyName == "FirstKey") shouldForceHide = true;
        if (GameManager.Instance.GetHasSecondKey() && keyName == "SecondKey") shouldForceHide = true;
        if (GameManager.Instance.GetHasThirdKey() && keyName == "ThirdKey") shouldForceHide = true;
        if (!GameManager.Instance.GetShouldShowKeys() || shouldForceHide) {
            boxCollider.enabled = false;
            spriteRenderer.enabled = false;
            return;
        }
        boxCollider.enabled = true;
        spriteRenderer.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (!collider.gameObject.CompareTag("Player")) return;
        if (keyName == "FirstKey") GameManager.Instance.SetHasFirstKey(true);
        if (keyName == "SecondKey") GameManager.Instance.SetHasSecondKey(true);
        if (keyName == "ThirdKey") GameManager.Instance.SetHasThirdKey(true);
        Destroy(gameObject);
    }
}
