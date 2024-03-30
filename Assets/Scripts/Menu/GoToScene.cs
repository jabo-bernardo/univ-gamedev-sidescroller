using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToScene : MonoBehaviour
{
    public void transitionToScene(string sceneName)
    {
        if (sceneName == null) return;
        StartCoroutine(FindObjectOfType<LevelLoader>().LoadLevel(sceneName));
    }

    public void transitionToIntroCut()
    {
        transitionToScene("IntroCutscene");
    }
}
