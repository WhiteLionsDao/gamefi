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
    private int health_min = 200;
    // public int health_uçan;
    // public int health_uzaktan;
    // public int health_büyük;
    private int health;
    private int health_defaulf;
    private float _timeBulletTouch = 2;
    private float _time = 2;

    [SerializeField] bool NPC1_küçük_yakından_vuran;
    [SerializeField] bool NPC2_uçarak_uzaktan_vuran;
    [SerializeField] bool NPC3_uzaktan_vuran;
    [SerializeField] bool NPC4_büyük_yakından_vuran;
    
    
    private void Awake()  // boolara göer canlarını ver
    {
        health_defaulf = health_min;

        player = GameObject.Find("Player");
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void TakeDamageEnmyMin(int dmg,GameObject enmyObject)
    {
        if (enmyObject.GetComponent<EnmyScipts>().NPC1_küçük_yakından_vuran && enmyObject.GetComponent<EnmyScipts>().health_min == 200)
        {
            health_min = health_min / 5;
        }
        if (enmyObject.GetComponent<EnmyScipts>().NPC2_uçarak_uzaktan_vuran && enmyObject.GetComponent<EnmyScipts>().health_min == 200)
        {
            health_min = health_min / 4;
        }
        if (enmyObject.GetComponent<EnmyScipts>().NPC3_uzaktan_vuran && enmyObject.GetComponent<EnmyScipts>().health_min == 200)
        {
            health_min = health_min / 4;
        }
        // if (enmyObject.GetComponent<EnmyScipts>().NPC4_büyük_yakından_vuran)
        // {
        //     health_min = health_min;
        // }
        health_min -= dmg;
        if (health_min <= 0)
        {
            Destroy(enmyObject);
            _gameManager.isEnmyDestroyedCount += 1;
            health_min = health_defaulf;  // ilk baştaki deger neyse onu veriyoruz
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
