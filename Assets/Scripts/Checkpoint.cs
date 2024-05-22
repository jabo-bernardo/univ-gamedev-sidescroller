using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter2D() {
        GameManager.Instance.SetLastCheckpoint(gameObject.transform.position);
    }
}
