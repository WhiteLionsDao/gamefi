using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] public Slider healthBar;


    public int health_player = 1500;  // bunu arrıtınca Playyer Conttrolerdan da arttır  ve Unitnin içiden bardan arttır

    private int dmg = 20;

    private void Awake()
    {
        healthBar.maxValue = health_player;
        healthBar.value = health_player;
    }

    private void Update()
    {
        healthBar.value = health_player;
    }
}
