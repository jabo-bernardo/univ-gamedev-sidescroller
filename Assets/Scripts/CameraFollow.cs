using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject targetObject;
    public Vector2 cameraBoundsMin;
    public Vector2 cameraBoundsMax;
    public float smoothScrollFactor = 0.2f;

    private float zOffset = 20f; 

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(
            Mathf.Clamp(targetObject.transform.position.x, cameraBoundsMin.x, cameraBoundsMax.x),
            Mathf.Clamp(targetObject.transform.position.y, cameraBoundsMin.y, cameraBoundsMax.y),
            -zOffset
        );
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothScrollFactor);
    }
}
