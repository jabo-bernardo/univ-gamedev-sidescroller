using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{

    private Slider slider; 
    public TMP_Text text;

    void Start() {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        slider.value = GameManager.Instance.GetPlayerHealth() / GameManager.Instance.GetPlayerMaxHealth();
        text.text = Math.Round(GameManager.Instance.GetPlayerHealth()) + " / " + Math.Round(GameManager.Instance.GetPlayerMaxHealth());
    }
}
