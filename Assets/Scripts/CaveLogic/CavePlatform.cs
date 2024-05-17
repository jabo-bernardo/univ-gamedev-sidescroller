using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavePlatform : MonoBehaviour
{
    void Update() {
        if (GameManager.Instance.GetShouldShowKeys()) {
            gameObject.SetActive(true);
            return;
        }
        gameObject.SetActive(false);
    }
}
