using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalOverhead : MonoBehaviour
{
    public string targetSceneName;

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            if (Input.GetMouseButton(0)) {
                StartCoroutine(FindObjectOfType<LevelLoader>().LoadLevel(targetSceneName));
            }
        }
    }
}
