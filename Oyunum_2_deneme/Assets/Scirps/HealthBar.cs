using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] public Slider healthBar;


    public int health_player = 200;

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
