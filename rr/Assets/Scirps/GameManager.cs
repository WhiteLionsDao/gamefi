using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int spawnBaseSize;
    [SerializeField] public int enmyGörüşMesafesi_x;
    [SerializeField] private int MaxHealth;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private PlayConttroler _playerController;
    [SerializeField] private ParticleSystem _lavEffect;
    [SerializeField] private ParticleSystem _VurusEffect;
    [SerializeField] private GameObject _player;
    
    // [SerializeField] GameObject _kücükMeteorlar;
    // [SerializeField] GameObject _büyükMeteorlar;
    // [SerializeField] GameObject _meteorOluşcagıYer;

    private Bullet _bulletScript;
    private float x_enmy;
    private float y_enmy;    
    private float x_player;
    private float y_player;
    private float x_sonuc;
    private float y_sonuc;
    private float isPlaceRight;
    public int isEnmyDestroyedCount = 0;
    private Rigidbody2D RB2_enmy;
    private bool isMovingInBaseRight = true;  // 1 den fazla enmy var ise hatalı çalışır.
    private float _timeTogether = 0;
    private float timeBtwShots;
    private float _timeJumpEnmy;
    private float _timeKüçükEnmy;
    private float _timeBüyükEnmy;
    private float startTimebtwShots = 25;

    [SerializeField] bool NPC1_küçük_yakından_vuran;
    [SerializeField] bool NPC2_uçarak_kamikaze_yapan;
    [SerializeField] bool NPC3_uzaktan_vuran;
    [SerializeField] bool NPC4_büyük_yakından_vuran;
    
    private GameObject ÇarpanGameObject;
    private List<GameObject> enemies = new List<GameObject>();   // bu listeye merminin değdigi enmy listesini yüklüyorsun.
    private bool isHit = false;
    private bool isHitPlayer = false;
    // ReSharper disable Unity.PerformanceAnalysis

    private void Update()
    {
        _scoreText.text = isEnmyDestroyedCount.ToString();
        timeBtwShots = timeBtwShots + Time.deltaTime;
        _timeJumpEnmy = _timeJumpEnmy + Time.deltaTime;
        _timeKüçükEnmy = _timeKüçükEnmy + Time.deltaTime;
        _timeBüyükEnmy = _timeBüyükEnmy + Time.deltaTime;
    }

    // private void FixedUpdate()
    // {
    //     MeteorScript();
    // }

    public void NPCFollow(GameObject enmy_gameObject, GameObject player_gameObject,float speed,int x_base,bool BirlikteHareketEtsinlermi, int Health, float _timeBulletTouch,float _time,
        bool NPC1, bool NPC2, bool NPC3, bool NPC4)
    {
        // bool _cevresiScriptsBool = enmy_gameObject.GetComponentInChildren<CevresiScripts>().enmyPlayerıGörüyor;    // burası hep false yolluyor    // dosyalar arasında bool'u yolladıgım her zaman hep false olarak yolluyor.
        // bool _enmyPlayerıGörüyorMu = enmy_gameObject.GetComponent<EnmyScipts>().enmyPlayerıGörüyorMu;   // burda BossunScripti olmadıgı için hata veriypr.
        GameObject[] Mermiler = GameObject.FindGameObjectsWithTag("Mermi");
        // List<GameObject> enemies = enmy_gameObject.GetComponentInChildren<CevresiScripts>().enemies;   // ben sadece vurdugum enmy'nin listeini istiyorum

        foreach (var mermi in Mermiler)
        {   // burda mermi Player'a çarpıyor ise Mermiyi yok et
            bool mermiIsTouching = mermi.GetComponent<Collider2D>().IsTouching(player_gameObject.GetComponent<Collider2D>());
            if (mermiIsTouching == true)
                Destroy(mermi);
        }
        if ((isHit == true) && (ÇarpanGameObject != null))  // burda Mermi Çarptıgında çevresindeki Enmylerin listesini ekliyor.
        {  // isHit == true ve ÇarpanGameObject != null   bunları koyzmazsam hep işlem yapıyor ve çöküyor.
            enemies.Clear();  // burda eski listeyi siliyorum, çünkü mermi çarptıgında yeni çevresindekiler ile işlem yapmam lazım.
            ÇarpanGameObject.GetComponent<EnmyScipts>().ZamanıSıfırla();
            foreach (var enemy in ÇarpanGameObject.GetComponentInChildren<CevresiScripts>().enemies) 
            {    // burda kaldın   // hiçbir mermi objeye çarpmadı içi burda null hatası veriyor.
                if (enemy != null)
                    enemies.Add(enemy);
                enemy.GetComponent<EnmyScipts>().ZamanıSıfırla();  // Hepsi listeye ekeledikten sonra, onların zamanını da sıfırlar.
            }
        }
        if (isHitPlayer == true)   
        {
            isHitPlayer = false;  // burda Mermi Player'a degerse false yapıyor. Belki ilerde kullanırsın.
        }
        
        x_enmy = enmy_gameObject.GetComponent<Transform>().position.x;
        y_enmy = enmy_gameObject.GetComponent<Transform>().position.y;
        x_player = player_gameObject.GetComponent<Transform>().position.x;
        y_player = player_gameObject.GetComponent<Transform>().position.y;
        RB2_enmy = enmy_gameObject.GetComponent<Rigidbody2D>();
        x_sonuc = x_enmy - x_player;
        y_sonuc = y_enmy - y_player;
        isPlaceRight = x_sonuc;
        if (isPlaceRight < 0) // player enmy'nin solunda ise isPlaceRight = -1 oluyor sağında ise 1 oluyor
        {
            isPlaceRight = isPlaceRight / isPlaceRight;
        }
        else
        {
            isPlaceRight = isPlaceRight / isPlaceRight;
            isPlaceRight = -isPlaceRight;
        }

        if (((x_sonuc <= enmyGörüşMesafesi_x) && (-enmyGörüşMesafesi_x <= x_sonuc)) && ((y_sonuc <= enmyGörüşMesafesi_x) && (-enmyGörüşMesafesi_x <= y_sonuc))) // enmy'i Player'ı görüyor
        { // yukardaki koşul = eger player yakınında ise çalışacaktır
            // print("çalışyıormu 1");
            if ((x_sonuc > 8) || (x_sonuc < -8)  && (NPC3))
            {  
                Move(speed, enmy_gameObject);
            }
            else if ((x_sonuc > 8) || (x_sonuc < -8)  &&  (NPC2))
            {
                Move(speed, enmy_gameObject);
            }
            
            if ((x_sonuc > 4) || (x_sonuc < -4) && (NPC4))
            {
                Move(speed, enmy_gameObject);
                //Büyük olan yere vursun
            }
            if ((x_sonuc > 4) || (x_sonuc < -4) && (NPC1))
            {
                Move(speed, enmy_gameObject);
            }
            
            if (NPC1 && _timeJumpEnmy > 0.5f)
            {
                AutoJump(enmy_gameObject,speed);
                _timeJumpEnmy = 0;
            }

            if (enmy_gameObject.GetComponent<Collider2D>().IsTouching(player_gameObject.GetComponent<Collider2D>()) && _timeKüçükEnmy > 1  && NPC1)
            {  
                _playerController.TakeDamagePlayer(20,player_gameObject);
                ParticleSystem _lavEffectOlustur = Instantiate(_lavEffect, _player.transform);
                ParticleSystem _FazladanVuruşEffectOlustur = Instantiate(_VurusEffect, _player.transform);
                Destroy(_lavEffectOlustur.gameObject, 5f);
                Destroy(_FazladanVuruşEffectOlustur.gameObject, 2f);
                _timeKüçükEnmy = 0;
            }
            if (enmy_gameObject.GetComponent<Collider2D>().IsTouching(player_gameObject.GetComponent<Collider2D>()) && _timeBüyükEnmy > 1  && NPC4)
            {  
                _playerController.TakeDamagePlayer(60,player_gameObject);
                ParticleSystem _lavEffectOlustur = Instantiate(_lavEffect, _player.transform);
                ParticleSystem _FazladanVuruşEffectOlustur = Instantiate(_VurusEffect, _player.transform);
                
                Destroy(_lavEffectOlustur.gameObject, 5f);
                Destroy(_FazladanVuruşEffectOlustur.gameObject, 2f);
                _timeBüyükEnmy = 0;
            }
            
            if ((_time > 1) && (NPC3))
            {
                Attack(enmy_gameObject,player_gameObject,speed);
                enmy_gameObject.GetComponent<EnmyScipts>().ZamanıSıfırla_time();   // _time = 0  yapar
            }
            if ((_time > 1) && (NPC2))
            {
                Attack(enmy_gameObject,player_gameObject,speed);
                enmy_gameObject.GetComponent<EnmyScipts>().ZamanıSıfırla_time();   // _time = 0  yapar
            }
            // print(enmyPlayerıGörüyorMu);
            // _enmyPlayerıGörüyorMu = true;
            // scriptin içindeki şeyi çagıracaksın. Collision degdi ise çalışacak olan kodu çagıracaksın.  Ve o kod enmy_gameObject childe nesnesinin içinde olucaktır.
        }
        else if ((ÇarpanGameObject != null) && (_timeBulletTouch < 2f))  // dmg alan enmy, çevresindeki enmyleride o tarafa dogru yönlendirir.
        {   // Mermi ÇarpanGameObject 'i yok ettiginden dolayı hata veriyor ondan  ÇarpanGameObject != null   yazdım.
            Move(speed,ÇarpanGameObject);  // mermi hangi objeye çarpıyorsa onuda hareket ettiriyor.
            foreach (var enemy in enemies) 
                Move(speed,enemy);

            isHit = false;
        }
        else if (!(((x_sonuc <= enmyGörüşMesafesi_x) && (-enmyGörüşMesafesi_x <= x_sonuc)) && ((y_sonuc <= 5) && (-5 <= y_sonuc))) && (_timeBulletTouch > 2f)) // _time < 2f  eger dmg alır ise 2sn bu fonksiyon çalışmayacak.
        { // yukardaki koşul = eger enmy, player'ın yakınında degil ise çalışacaktır
            // print("çalışıyorumu 2");
            // _enmyPlayerıGörüyorMu = false;
            if ((x_enmy <= x_base + spawnBaseSize) && (x_enmy >= x_base) || isPlaceRight ==1) // enmy Base'in içinde ise çalışacak ve Player enmy'nin sağına geçmiş ise çalışacak
            {  // 20 <= x_enmy <= 30
                // print("çalışyıormu 3");
                
                if (BirlikteHareketEtsinlermi == true)
                    MoveingInBaseTogether(speed, x_base, enmy_gameObject);  // enmy'lerin: eş zamanlı olarak, base'in içinde yürümesini sagla
                else
                    MoveingInBase(speed, x_base, enmy_gameObject);  // enmy'lerin: eş zamanlı olmadan, base'in içinde yürümesini saglar
            }
            else if (!((x_enmy <= x_base + spawnBaseSize) && (x_enmy >= x_base)) && (isPlaceRight == -1))  // enmy base'in dışında ise çalışacak ve Player enmy'nin solunda olmak şartı ile çalışacak
            {  // 20 <= x_enmy <= 30    
                // print("çalışyıormu 4");
                GoToBase(speed, x_base, enmy_gameObject);
            }
        }
    }
    public void Move(float speed , GameObject enmy_gameObject)
    {
        RB2_enmy = enmy_gameObject.GetComponent<Rigidbody2D>();
        RB2_enmy.velocity = new Vector2(isPlaceRight * speed,RB2_enmy.velocity.y);
        if (RB2_enmy.velocity.x < 0)  // karakterin sola dogru hız kazanıyorsa yönüde sola dogru döner
            enmy_gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
        if (RB2_enmy.velocity.x > 0)  // karakterin sağa dogru hız kazanıyorsa yönüde sağa dogru döner
            enmy_gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0, 180, 0);
    }

    void GoToBase(float speed, int x_base, GameObject enmy_gameObject)
    {
        PlayerLocationİs_LeftEnmy(speed,x_base,enmy_gameObject);
        PlayerLocationİs_RightEnmy(speed,x_base,enmy_gameObject);
    }  // eger player enmy'nin sagında ise enmy'i sağa çevir  hata buydu ***
    void MoveingInBase(float speed ,int x_base, GameObject enmy_gameObject) // base'in içinde hareket ettiren fonksiyon
    {
        // aşagıdaki y: -180 olur ise -1 degerini alıyor. 360 veya 0 olursa 0 degerini alıyor.
        float y = enmy_gameObject.transform.rotation.y;  // 0 ila -1 arasında bir deger alıyor.
        if (x_player < x_enmy)  // player enmy'in solunda ise çalışır
        {
            if ((y == 0))  // enmy base'in en sagına gelene kadar çalışacak
                MoveingRight(speed / 2 , enmy_gameObject);
            else if ((y == -1))  // enmy base'in en soluna gelene kadar çalışacak
                MoveingLeft(speed / 2 , enmy_gameObject);
        }
        if (x_player > x_enmy) // player enmy'in sağında ise çalışacak. Enmy Player'ı takip etsin.
            Move(speed, enmy_gameObject);
    }
    void MoveingInBaseTogether(float speed ,int x_base, GameObject enmy_gameObject)   // bu kodta player enmy'nin sağında ise gitmiyor hata veriyor.
    {
        _timeTogether = _timeTogether + Time.deltaTime;
        
        if ((isMovingInBaseRight == true && _timeTogether > 1.5))  // enmy base'in en sagına gelene kadar çalışacak
        {
            // print(" 1 çalışıyor");
            MoveingRight(speed / 2 , enmy_gameObject);
            if (x_enmy >= x_base + spawnBaseSize -1) // enmy base'in -en sağından- 1 adım geride ise çalışacak
            {
                _timeTogether = 0;
                isMovingInBaseRight = false;
                // print("kaç kez çalıştı 1");
            }
        }
        if ((isMovingInBaseRight == false && _timeTogether > 1.5))  // enmy base'in en soluna gelene kadar çalışacak
        {
            // print(" 2 çalışıyor");
            MoveingLeft(speed / 2 , enmy_gameObject);
            if (x_enmy <= x_base + 1) //  enmy base'in -en solundan- 1 adım ileride ise çalışacak
            {
                _timeTogether = 0;
                isMovingInBaseRight = true;
                // print("kaç kez çalıştı 2");
            }
        }
    }
    void MoveingRight(float speed, GameObject enmy_gameObject) // sağa dogru yürüyor
    {
        enmy_gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0,0,0);  // karakteri saga dogru döndürüyor
        RB2_enmy.velocity = new Vector2(-isPlaceRight * speed, RB2_enmy.velocity.y);
    }

    void MoveingLeft(float speed, GameObject enmy_gameObject)  // sola dogru yürüyor
    {
        enmy_gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0,180,0);  // karakteri sola dogru döndürüyor
        RB2_enmy.velocity = new Vector2(isPlaceRight * speed, RB2_enmy.velocity.y);
    }

    void PlayerLocationİs_LeftEnmy(float speed, int x_base, GameObject enmy_gameObject)  // base'in içinde gezmeye yarar
    {
        if (x_player < x_enmy)  // player enmy'in solunda ise çalışır
        {
            if ((x_enmy < x_base))  // Sağa gider
                MoveingRight(speed, enmy_gameObject);
            if ((x_enmy > x_base))  // Sola gider
                MoveingLeft(speed, enmy_gameObject);
        }
    }

    void PlayerLocationİs_RightEnmy(float speed, int x_base, GameObject enmy_gameObject)  // base'in içinde gezmeye yarar
    { 
        if (x_player > x_enmy)  // player enmy'in sağında ise çalışır
        {
            if (x_enmy < x_base + spawnBaseSize)  // Sola gider
                MoveingLeft(speed, enmy_gameObject);
            if (x_enmy > x_base + spawnBaseSize)  // Sağa gider
                MoveingRight(speed, enmy_gameObject);
        }
    }

    public void isHitChange()
    {
        isHit = true;
    }

    public void HangiGameObjesiÇarptı(GameObject _ÇarpanGameObjesi)
    {
        ÇarpanGameObject = _ÇarpanGameObjesi;
    }

    
    
    void Attack(GameObject enmy_gameObject, GameObject player_gameObject,float speed)
    {   // speed merminin hızı
        Transform Namlu = enmy_gameObject.transform.Find("Namlu");
        GameObject Mermi = Instantiate(Bullet, Namlu.position,transform.rotation);
        
        Vector2 MermininGidecegiYer = new Vector2(player_gameObject.transform.position.x, player_gameObject.transform.position.y);
        Vector2 hizVektoru = (MermininGidecegiYer - (Vector2)Mermi.transform.position).normalized * (speed * 5);  // buralar mermiye hız verir
        Mermi.GetComponent<Rigidbody2D>().velocity = hizVektoru;
        Destroy(Mermi,1f);
    }

    public void isHitPlayerChange()
    {
        isHitPlayer = true;
    }

    void AutoJump(GameObject enmy_gameObject,float speed)
    {
        
        int rasgele;
        rasgele = Random.Range(1, 4);
        if (rasgele == 1)
        {
            enmy_gameObject.GetComponent<Rigidbody2D>().velocity =
                new Vector2(enmy_gameObject.GetComponent<Rigidbody2D>().velocity.x, 1 * speed);
        }
    }

    // void MeteorScript()
    // {
    //     _kücükMeteorlar.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
    //     _büyükMeteorlar.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.2f, 0.3f);
    //     if (_kücükMeteorlar.transform.position.x <  -40)
    //     {
    //         Destroy(_kücükMeteorlar.gameObject);
    //         Oluştur(_kücükMeteorlar);
    //     }
    //     if (_büyükMeteorlar.transform.position.x <  -55)
    //     {
    //         Destroy(_büyükMeteorlar.gameObject);
    //         Oluştur(_büyükMeteorlar);
    //     }
    // }
    // void Oluştur(GameObject Meteor)
    // {
    //     Instantiate(Meteor, _meteorOluşcagıYer.transform.position, transform.rotation);
    // }
}

