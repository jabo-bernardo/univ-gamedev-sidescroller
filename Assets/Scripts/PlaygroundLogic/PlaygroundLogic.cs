using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaygroundLogic : MonoBehaviour
{
    public GameObject fightSceneBarrier;
    public GameObject castleBarrier;
    public GameObject spawners;
    public Vector2 cameraBoundsMin;
    public Vector2 cameraBoundsMax;

    void Update()
    {
        int banditsKilled = GameManager.Instance.killTracker.ContainsKey("Bandit(Clone)") ? GameManager.Instance.killTracker["Bandit(Clone)"] : 0;

        bool TAVERN_ACTORS_KILLED = banditsKilled >= 2;
        bool IS_READY_TO_EXPLORE = GameManager.Instance.GetHasTalkedWithBartender() && TAVERN_ACTORS_KILLED;
        bool SHOULD_SPAWN_ENEMY = GameManager.Instance.GetHasTalkedWithBartender() && !(TAVERN_ACTORS_KILLED);
        if (IS_READY_TO_EXPLORE) {
            fightSceneBarrier.SetActive(false);
            spawners.SetActive(false);
        } else if (SHOULD_SPAWN_ENEMY) {
            spawners.SetActive(true);
        } else {
            fightSceneBarrier.SetActive(true);
            CameraFollow camFollow = GameObject.Find("MainCamera").GetComponent<CameraFollow>();
            camFollow.isLocked = true;
            camFollow.temporaryCameraBoundsMin = cameraBoundsMin;
            camFollow.temporaryCameraBoundsMax = cameraBoundsMax;
        }
        
        if (GameManager.Instance.GetHasCastleKey()) {
            castleBarrier.SetActive(false);
        }
    }
}
