using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CevresiScripts : MonoBehaviour
{
    [SerializeField] GameObject _cevresi;
    private GameManager _gameManager;
    private EnmyScipts _enmyScipts;
    private GameObject parentObject_enmy;
    public bool enmyPlayerıGörüyor = false;
    
    public List<GameObject> enemies = new List<GameObject>();
    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        parentObject_enmy = _cevresi.transform.parent.gameObject;
    }

    private void Update()
    {
        // print(" Cevres: " + parentObject_enmy.GetComponent<EnmyScipts>().enmyPlayerıGörüyorMu); // bool da sıkıntı var
    }

    
    
    private void OnTriggerEnter2D(Collider2D other)   // büyük alanın içine girince listeye ekliyor
    {
        if (other.gameObject.CompareTag("Enmy")) 
            enemies.Add(other.gameObject);     // burda liste yaparken gameObject olarak almıyor nedense, nedenini bilmiyorum.
        // foreach (var enemy in enemies)
        // {
            // print("Listede olanlar 1 : "+ enemy);
            // print("Bool: "+ (enemy));
        // }
    }

    private void OnTriggerExit2D(Collider2D other)   // büyük alandan çıkınca listeden çıkarıyor
    {
        if (other.gameObject.CompareTag("Enmy"))
            enemies.Remove(other.gameObject);
        // foreach (var enemy in enemies)
        //     print("Listede olanlar 2 : " + enemy);
    }
}
