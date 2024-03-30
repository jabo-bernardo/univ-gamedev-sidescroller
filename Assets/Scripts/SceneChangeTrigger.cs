using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeTrigger : MonoBehaviour
{
    public string targetSceneName;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            StartCoroutine(FindObjectOfType<LevelLoader>().LoadLevel(targetSceneName));
        }
    }
}
