using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMermiScript : MonoBehaviour
{
    [SerializeField] private GameObject _vuruşEfect;
    [SerializeField] private GameObject bullet;
    private EnmyScipts _enmyScipts;
    private GameManager _gameManager;
    

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enmy"))
        {
            _enmyScipts = GameObject.FindGameObjectWithTag("Enmy").GetComponent<EnmyScipts>();    // burda mermi ile hangi GameObject çarptı ise onu GameManagera yolla.
            // yukardakini buraya yazmazsam hata veriyor. 
            _gameManager.HangiGameObjesiÇarptı(other.gameObject); //çarpan gameObjesi GameManagera yolluyorum  
            // foreach (var enemy in other.gameObject.GetComponentInChildren<CevresiScripts>().enemies)  // burda başka yerdeki listeyi bu dosyadaki listeye aktarıyoruz.
            //      enemies.Add(enemy);
            _gameManager.isHitChange();  // enmylerden biri dmg alır ise bool true olur.
            // StartCoroutine(MoveEnemies());  // çevresindeki enmyleri player'a dogru hareket ettiriyor.
            GameObject Effect = Instantiate(_vuruşEfect, other.gameObject.transform.position,transform.rotation);
            Destroy(Effect,1f);
            _enmyScipts.TakeDamageEnmyMin(20,other.gameObject);
            // print(_enmyScipts.health_min);
            Destroy(bullet);   // bunu yok ettigimiz için, kod ta bu dosyanın içinde oldugu için hepsi çalışmıyor. 
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(bullet);
        }        
        if (other.gameObject.CompareTag("ZeminTag"))
        {
            Destroy(bullet);
        }
    }


}
