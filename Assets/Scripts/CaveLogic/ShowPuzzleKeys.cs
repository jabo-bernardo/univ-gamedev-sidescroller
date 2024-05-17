using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPuzzleKeys : MonoBehaviour
{
    void OnTriggerEnter2D()
    {
        GameManager.Instance.SetShouldShowKeys(true);
    }
}
