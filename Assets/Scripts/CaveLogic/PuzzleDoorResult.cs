using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (SpriteRenderer))]
public class PuzzleDoorResult : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameObject portalDoor;
    public Sprite spriteUnlocked;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        string keyName = gameObject.name;

        if (keyName == "FirstKeyResult" && GameManager.Instance.GetHasFirstKey()) {
            spriteRenderer.sprite = spriteUnlocked;
            if (portalDoor) {
                portalDoor.SetActive(true);
            }
        }

        if (keyName == "SecondKeyResult" && GameManager.Instance.GetHasSecondKey()) {
            spriteRenderer.sprite = spriteUnlocked;
            if (portalDoor) {
                portalDoor.SetActive(true);
            }
        }

        if (keyName == "ThirdKeyResult" && GameManager.Instance.GetHasThirdKey()) {
            spriteRenderer.sprite = spriteUnlocked;
            if (portalDoor) {
                portalDoor.SetActive(true);
            }
        }
    }
}
