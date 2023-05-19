using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnmyScipts : MonoBehaviour
{
    [SerializeField] public GameObject enmy;
    [SerializeField] private float speed;
    [SerializeField] private int x_base;
    [SerializeField] private bool BirlikteHareketEtsinlermi;
    public bool enmyPlayerıGörüyorMu = true;
    private GameObject player;
    private GameManager _gameManager;
    public int health_min;
    private int health_min_defaulf;
    private float _timeBulletTouch = 2;
    private float _time = 2;

    [SerializeField] bool NPC1_küçük_yakından_vuran;
    [SerializeField] bool NPC2_uçarak_uzaktan_vuran;
    [SerializeField] bool NPC3_uzaktan_vuran;
    [SerializeField] bool NPC4_büyük_yakından_vuran;
    
    
    private void Awake()
    {
        player = GameObject.Find("Player");
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        health_min_defaulf = health_min;
    }
    public void TakeDamageEnmyMin(int dmg,GameObject enmyObject)
    {
        health_min -= dmg;
        if (health_min <= 0)
        {
            Destroy(enmyObject);
            _gameManager.isEnmyDestroyedCount += 1;
            health_min = health_min_defaulf;
        }
    }

    public void ZamanıSıfırla()
    {
        _timeBulletTouch = 0;
    }
    public void ZamanıSıfırla_time()
    {
        _time = 0;
    }

    private void Update()
    {
        _timeBulletTouch = _timeBulletTouch + Time.deltaTime;
        _time = _time + Time.deltaTime;
    }

    private void FixedUpdate()
    {
        _gameManager.NPCFollow(enmy,player,speed,x_base,BirlikteHareketEtsinlermi,health_min,_timeBulletTouch,_time,
            NPC1_küçük_yakından_vuran,NPC2_uçarak_uzaktan_vuran,NPC3_uzaktan_vuran,NPC4_büyük_yakından_vuran);
    }

    
}
