using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondaryAttack : MonoBehaviour
{
    private Player player;
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
        player = FindAnyObjectByType<Player>();
    }
    void Update()
    {
        if (player.GetIsSecondaryCooldown()) {
            Color newColor = image.color;
            newColor.a = 0.2f;
            image.color = newColor;
        } else {
            image.color = Color.white;
        }
    }
}
