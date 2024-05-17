using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeerCount : MonoBehaviour
{

    private TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();   
    }

    void Update()
    {
        text.text = GameManager.Instance.GetBeerCount() + "";
    }
}
