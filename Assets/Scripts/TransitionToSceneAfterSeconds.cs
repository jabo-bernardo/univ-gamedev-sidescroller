using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToSceneAfterSeconds : MonoBehaviour
{
    public float afterXSeconds;
    public string sceneName;

    public void Start()
    {
        StartCoroutine(countTimeBomb());
    }

    IEnumerator countTimeBomb()
    {
        yield return new WaitForSeconds(afterXSeconds);

        StartCoroutine(FindObjectOfType<LevelLoader>().LoadLevel(sceneName));
    }
}
