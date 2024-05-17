using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public IEnumerator LoadLevel(string sceneName) {
        transition.SetTrigger("Start");
        Debug.Log("Loading Scene " + sceneName);

        GameObject player = GameObject.Find("Player");
        GameManager gm = GameManager.Instance;
        if (player) {
              string currentScenename = SceneManager.GetActiveScene().name;
            if (gm.playerSavedLocationsBeforeTeleport.ContainsKey(currentScenename))
                gm.playerSavedLocationsBeforeTeleport.Remove(currentScenename);
            gm.playerSavedLocationsBeforeTeleport.Add(currentScenename, player.transform.position);
        }

      

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
        if (player) {
            GameObject newPlayerLoaded = GameObject.Find("Player");
            if (gm.playerSavedLocationsBeforeTeleport.ContainsKey(sceneName)) {
                newPlayerLoaded.transform.position = gm.playerSavedLocationsBeforeTeleport.GetValueOrDefault(sceneName);
            }
        }
    }

    public IEnumerator LoadLevelByIndex(int index) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);
    }
}
