using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerMinEnmy : MonoBehaviour
{
    [SerializeField] private GameObject spawnerMinEnmy;
    [SerializeField] private GameObject enmyMin;
    [SerializeField] private GameObject enmyBoss;
    [SerializeField] private int count = 5;
    private EnmyScipts _enmyScipts;
    private GameManager _gameManagerScripts;
    

    private void Awake()
    {
        _enmyScipts = enmyMin.GetComponent<EnmyScipts>();
        _gameManagerScripts = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private int i = 0;
    private int j = 0;
    void SpawnMinEnmy()
    {
        if (i <= count)
        {
            Invoke("Spaw",1f);  // burda hata var
            i++;
            
        }
        else if (_gameManagerScripts.isEnmyDestroyedCount >= 5 && j < 1)
        {
            j++;
            Instantiate(enmyBoss, spawnerMinEnmy.transform);
            print("Tüm Spawnı öldürdün");
        }
    }

    private void Spaw()
    {
        Instantiate(enmyMin, spawnerMinEnmy.transform);
    }

    private void FixedUpdate()
    {
        SpawnMinEnmy();
    }
}
