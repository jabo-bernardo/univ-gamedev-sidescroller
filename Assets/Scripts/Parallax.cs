using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    private float bgLength, startPos;
    public GameObject mainCamera;
    public float parallaxIntensity;
    public bool shouldLockYToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        bgLength = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        
        float distance = mainCamera.transform.position.x * parallaxIntensity;

        transform.position = new Vector3 (startPos + distance, transform.position.y, transform.position.z);
        if (shouldLockYToPlayer)
        {
            transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y - 3, transform.position.z);
        }

        
    }
}
