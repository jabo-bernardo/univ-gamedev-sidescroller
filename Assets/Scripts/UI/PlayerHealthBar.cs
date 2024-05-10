using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{

    private Slider slider; 

    void Start() {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        slider.value = GameManager.Instance.GetPlayerHealth() / GameManager.Instance.GetPlayerMaxHealth();
    }
}
