using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToSceneAfterSeconds : MonoBehaviour
{
    public float afterXSeconds;
    public string sceneName;
    public bool isTransitioning;

    public void Start()
    {
        StartCoroutine(countTimeBomb());
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !isTransitioning) {
            isTransitioning = true;
            StartCoroutine(FindObjectOfType<LevelLoader>().LoadLevel(sceneName));
        }
    }

    IEnumerator countTimeBomb()
    {
        yield return new WaitForSeconds(afterXSeconds);

        StartCoroutine(FindObjectOfType<LevelLoader>().LoadLevel(sceneName));
    }
}
