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

        GameObject player = GameObject.Find("Player");

        string currentScenename = SceneManager.GetActiveScene().name;
        GameManager gm = GameManager.Instance;
        if (gm.playerSavedLocationsBeforeTeleport.ContainsKey(currentScenename))
            gm.playerSavedLocationsBeforeTeleport.Remove(currentScenename);
        gm.playerSavedLocationsBeforeTeleport.Add(currentScenename, player.transform.position);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
        GameObject newPlayerLoaded = GameObject.Find("Player");
        if (gm.playerSavedLocationsBeforeTeleport.ContainsKey(sceneName)) {
            newPlayerLoaded.transform.position = gm.playerSavedLocationsBeforeTeleport.GetValueOrDefault(sceneName);
        }
    }
}
