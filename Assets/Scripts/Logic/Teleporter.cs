using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleporterCheckpoint;
    public string targetTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (teleporterCheckpoint == null) return;
        if (!collision.gameObject.CompareTag(targetTag)) return;
        collision.gameObject.transform.SetPositionAndRotation(teleporterCheckpoint.position, Quaternion.identity);
    }
}
