using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleLogic : MonoBehaviour
{
    public GameObject[] enemiesToKill;
    public GameObject portalDoor;

    // Update is called once per frame
    void Update()
    {
        bool IS_STAGE_BEATEN = true;

        foreach (GameObject enemy in enemiesToKill) {
            if (enemy) IS_STAGE_BEATEN = false;
        }
        if (!IS_STAGE_BEATEN) {
            portalDoor.SetActive(false);
            return;
        }
        portalDoor.SetActive(true);
    }
}

