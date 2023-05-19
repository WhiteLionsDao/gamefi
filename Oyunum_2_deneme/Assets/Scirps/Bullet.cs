using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour    // merminin position da sıkıntı var
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
        if (other.gameObject.CompareTag("Player"))
        {
            _gameManager.isHitPlayerChange();
            GameObject Effect = Instantiate(_vuruşEfect, other.gameObject.transform.position,transform.rotation);
            Destroy(Effect,1f);
            other.gameObject.GetComponent<PlayConttroler>().TakeDamagePlayer(20,other.gameObject); // burda hata olabilir.    // burayı can barına bagla
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        // if (other.gameObject.CompareTag("Enmy"))
        // {
        //     _enmyScipts = GameObject.FindGameObjectWithTag("Enmy").GetComponent<EnmyScipts>();    // burda mermi ile hangi GameObject çarptı ise onu GameManagera yolla.
        //     // yukardakini buraya yazmazsam hata veriyor. 
        //     _gameManager.HangiGameObjesiÇarptı(other.gameObject); //çarpan gameObjesi GameManagera yolluyorum  
        //     // foreach (var enemy in other.gameObject.GetComponentInChildren<CevresiScripts>().enemies)  // burda başka yerdeki listeyi bu dosyadaki listeye aktarıyoruz.
        //     //      enemies.Add(enemy);
        //     _gameManager.isHitChange();  // enmylerden biri dmg alır ise bool true olur.
        //     // StartCoroutine(MoveEnemies());  // çevresindeki enmyleri player'a dogru hareket ettiriyor.
        //     GameObject Effect = Instantiate(_vuruşEfect, other.gameObject.transform.position,transform.rotation);
        //     Destroy(Effect,1f);
        //     _enmyScipts.TakeDamageEnmyMin(20,other.gameObject);
        //     // print(_enmyScipts.health_min);
        //     Destroy(bullet);   // bunu yok ettigimiz için, kod ta bu dosyanın içinde oldugu için hepsi çalışmıyor. 
        // }
        // if (other.gameObject.tag == "EnmyBoss")
        // {
        //     _enmyBossScripts = GameObject.FindGameObjectWithTag("EnmyBoss").GetComponent<EnmyBossScripts>();
        //     // yukardakini buraya yazmazsam hata veriyor. 
        //     Instantiate(_vuruşEfect, _enmyBossScripts.enmyBoss.transform);
        //     _enmyBossScripts.TakeDamageEnmyBoss(20);
        //     // print(_enmyBossScripts.health_boss);
        //     Destroy(bullet);
        // }

    }

    //
    // MoveEnemies GameManagerdan çagırman lazım
    //     listeyi aktardık ve bu koda geçirebildim.  // GameManagerda GoToBase sıkıntı çıkarıyor.
        
        
        
        // private IEnumerator MoveEnemies()
        // {
        //     foreach (var enemy in enemies)   // liste ve foreach'i ögrenicem    // burda sıkıntı var
        //     {
        //         
        //         if (enemy != gameObject && Vector2.Distance(enemy.transform.position, transform.position) <= interactionDistance)
        //         {   // enemy != gameObject   deme nedenim: mermiyi yiyen enmy'nin etkilenmemsi ile alaklı birşey, ama bunu silsekte şimdilil ginede etkilenmez.
        //             Vector2 direction = (Vector2)enemy.transform.position - (Vector2)transform.position;  // merminin çarptıgı yer ile enmy'nin mesafesini ölçer
        //             enemy.GetComponent<Rigidbody2D>().velocity = -direction.normalized * ÇevresinsekiEnmyÇekmeSpeed;   // hangi yöndeyse o yöne dogru hız uyguluyor.
        //         }
        //     }
        //
        //     print("bekliyorum 2");
        //     yield return new WaitForSeconds(1f); // vuruyosun ve 1 saniye boyunca yanına dogru yyürüyorlar.
        //     print("bekliyorum 108888");
        //     foreach (var enemy in enemies)
        //     {
        //         if (enemy != gameObject && Vector2.Distance(enemy.transform.position, transform.position) <= interactionDistance)
        //         {
        //             enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;  // 1 sn sonrada hızını durduruyor.
        //         }
        //     }
        //
        //     enemies.Clear();  // çevresindeki listesini tuttugu enmyleri siliyor.
        //     isHit = false;
        //     yield return null;
        // }
}
