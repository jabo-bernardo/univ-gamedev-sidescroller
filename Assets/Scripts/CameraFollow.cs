using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraFollow : MonoBehaviour
{
    public GameObject targetObject;
    public Vector2 cameraBoundsMin;
    public Vector2 cameraBoundsMax;

    public Vector2 temporaryCameraBoundsMin;
    public Vector2 temporaryCameraBoundsMax;

    public float smoothScrollFactor = 0.2f;
    public bool isCinematic = false;
    public bool isLocked = false;
    public int cameraZoom = 38;

    private float zOffset = 20f; 
    private PixelPerfectCamera pixelPerfectCamera;

    void Start() {
        pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
    }

    void FixedUpdate()
    {
        float camBoundsMinX = cameraBoundsMin.x;
        float camBoundsMinY = cameraBoundsMin.y;
        float camBoundsMaxX = cameraBoundsMax.x;
        float camBoundsMaxY = cameraBoundsMax.y;
        int camZoom = cameraZoom;
        if (isCinematic) {
            camBoundsMinX = -99999;
            camBoundsMinY = -99999;
            camBoundsMaxX = 99999;
            camBoundsMaxY = 99999;
            camZoom = 50;            
        }
        if (isLocked) {
            camBoundsMinX = temporaryCameraBoundsMin.x;
            camBoundsMinY = temporaryCameraBoundsMin.y;
            camBoundsMaxX = temporaryCameraBoundsMax.x;
            camBoundsMaxY = temporaryCameraBoundsMax.y;
        }

        pixelPerfectCamera.assetsPPU = camZoom;
        Vector3 targetPosition = new Vector3(
            Mathf.Clamp(targetObject.transform.position.x, camBoundsMinX, camBoundsMaxX),
            Mathf.Clamp(targetObject.transform.position.y, camBoundsMinY, camBoundsMaxY),
            -zOffset
        );
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothScrollFactor);
    }
}
