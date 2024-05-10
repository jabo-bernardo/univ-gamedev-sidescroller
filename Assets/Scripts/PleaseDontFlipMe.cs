using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PleaseDontFlipMe : MonoBehaviour
{
    public GameObject parent;

    void Start() {
        if (!parent) parent = transform.parent.gameObject;
    }

    void Update() {
        if (parent.transform.localScale.x < 0) {
            transform.localScale = new Vector3(-1 * Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else {
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
