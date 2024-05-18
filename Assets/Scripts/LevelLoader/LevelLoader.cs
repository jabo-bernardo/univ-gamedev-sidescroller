using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (AudioSource))]
public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    private AudioSource audioSource;
    private bool isSceneLoading;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator LoadLevel(string sceneName) {
        if (isSceneLoading) yield return null;
        transition.SetTrigger("Start");
        Debug.Log("Loading Scene " + sceneName);
        isSceneLoading = true;
             

        GameObject player = GameObject.Find("Player");
        GameManager gm = GameManager.Instance;
        if (player) {
              string currentScenename = SceneManager.GetActiveScene().name;
            if (gm.playerSavedLocationsBeforeTeleport.ContainsKey(currentScenename))
                gm.playerSavedLocationsBeforeTeleport.Remove(currentScenename);
            gm.playerSavedLocationsBeforeTeleport.Add(currentScenename, player.transform.position);
        }

        audioSource.Play();
      

        yield return new WaitForSeconds(transitionTime);
           

        SceneManager.LoadScene(sceneName);
        if (player) {
            GameObject newPlayerLoaded = GameObject.Find("Player");
            if (gm.playerSavedLocationsBeforeTeleport.ContainsKey(sceneName)) {
                newPlayerLoaded.transform.position = gm.playerSavedLocationsBeforeTeleport.GetValueOrDefault(sceneName);
            }
        }
        isSceneLoading = false;
    }

    public IEnumerator LoadLevelByIndex(int index) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);
    }
}
