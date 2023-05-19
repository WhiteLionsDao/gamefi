using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class PlayerManager : MonoBehaviour
{
        public PlayConttroler _playConttroler;
        private GameManager _gameManager;
        [SerializeField] ParticleSystem _alev;
        [SerializeField] GameObject Player;
        [SerializeField] Rigidbody2D RB2;
        [SerializeField] float movedSpeed;
        
        
        private float jetpackForce = 50;
        private Transform _JetPack;
        private float _time;
        private float _time2;
        private float _timebomb;
        private float _timeBoot;

        private float KarakterinBaktıgıyön;
        
        private float yatayEksen;
        private float dikeyEksen;
        
        private float dashSayac;
        private float jumpSayac;

        [SerializeField] private GameObject BombPanel;
        
        [SerializeField] float fieldOfImpact;
        [SerializeField] float force;
        [SerializeField] LayerMask layerToHit;

        [SerializeField] private ParticleSystem AyakkabıEffect;
        [SerializeField] private ParticleSystem BombEffect;
        public bool SagYon { get; set; } = true; // Prop kullanmiyorsan awake metodunda ilklendirme yapabilirsin
        public GameObject firlatilacakPrefab; // F�rlat�lacak nesnenin prefab�
        public Transform firlatmaNoktasi; // F�rlatma noktas�
        public float firlatmaGucu = 10f; // F�rlatma g�c�
        public GameObject firlatilanNesne;
        [SerializeField] GameObject BootPanel;
        private bool _IsActiveIron;
        private bool _efectAyakkabı;
        
        private int isTocuhTheFloar = 0;

        private void Awake()
        { 
                _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
                _JetPack = Player.transform.Find("Jetpack");
        }

        private void Update()
        {
                _time = _time + Time.deltaTime;
                _time2 = _time2 + Time.deltaTime;
                _timebomb = _timebomb + Time.deltaTime;
                _timeBoot = _timeBoot + Time.deltaTime;
                KarakterinBaktıgıyön = Player.transform.localScale.x;
                if (_timebomb > 2.5f)
                {
                        BombPanel.SetActive(false);
                }
                if (isTocuhTheFloar == 1 && _IsActiveIron == true && _efectAyakkabı ==  true)
                {
                        ParticleSystem EffectAykkabı = Instantiate(AyakkabıEffect, Player.transform.position, transform.rotation);
                        Destroy(EffectAykkabı.gameObject, 2f);
                        Explode();
                        _efectAyakkabı = false;
                }
        }

        private void FixedUpdate()
        {                                       
                isTocuhTheFloar = _playConttroler.isTocuhTheFloar;
                yatayEksen = Input.GetAxisRaw("Horizontal");
                dikeyEksen = Input.GetAxis("Vertical");
                dashSayac = dashSayac + Time.deltaTime;
                jumpSayac = jumpSayac + Time.deltaTime;
                if (Input.GetButton("Horizontal"))  // move to horizontal
                {
                        RB2.velocity = new Vector2(yatayEksen * movedSpeed , RB2.velocity.y);
                        // RB2.AddForce(Vector2.right * (yatayEksen * 10));
                        // RB2.MovePosition((Vector2)Player.transform.position + eksenler * (5 * Time.deltaTime));
                }
                if (Input.GetKeyDown(KeyCode.O) && dashSayac > 1)  // dash skills
                {
                        dashSayac = 0;
                        RB2.velocity = new Vector2(yatayEksen * 100f, 0f);
                }
                if (Input.GetButton("Vertical") && jumpSayac > 1 && isTocuhTheFloar == 1)  // jumping
                {
                        isTocuhTheFloar = 0;
                        CompareTag("Player");
                        jumpSayac = 0;
                        RB2.velocity = new Vector2(0f,5f);
                }

                if (Input.GetKey(KeyCode.Space))
                {
                        JetPack();
                }
                if (Input.GetKey(KeyCode.B))
                {
                        Firlat();
                }
                if (Input.GetKey(KeyCode.U))  // time ekle
                {
                        IronBoot();
                }
        }
        void JetPack()
        {
                RB2.AddForce(new Vector2(RB2.velocity.x, jetpackForce));   // buraya jetpack skilini yaz
                if (_time > 0.3f)
                {
                        ParticleSystem JetAlevi = Instantiate(_alev,_JetPack);
                        Destroy(JetAlevi.GameObject(),5f);
                        _time = 0;
                }

                if (_time2 > 0.5f)
                {
                        ParticleSystem JetAlevi = Instantiate(_alev,_JetPack.position,transform.rotation);
                        Destroy(JetAlevi.GameObject(),1f);
                        _time = 0;
                }
        }
        // ReSharper disable Unity.PerformanceAnalysis
        void Firlat()
        {
                
                if (_timebomb >= 2.5)
                {
                        BombPanel.SetActive(true);
                        firlatilanNesne = Instantiate(firlatilacakPrefab, firlatmaNoktasi.position, Quaternion.identity);
                        Rigidbody2D rb = firlatilanNesne.GetComponent<Rigidbody2D>();
                        rb.AddForce((SagYon ? Vector2.right : Vector2.left) * firlatmaGucu * KarakterinBaktıgıyön, ForceMode2D.Impulse);
                        Invoke("BombPatlamaEffect",1.5f);
                        _timebomb = 0;
                }
        }

        void BombPatlamaEffect()
        {
                ParticleSystem _bombEffect = Instantiate(BombEffect, firlatilanNesne.transform.position,transform.rotation);
                Destroy(_bombEffect.GameObject(),2f);
        }
        
        public void Explode()
        {
                Collider2D[] objects = Physics2D.OverlapCircleAll(Player.transform.position, fieldOfImpact, layerToHit);
                foreach (Collider2D obj in objects)
                {
                        Vector2 direction = obj.transform.position - Player.transform.position;
                        obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
                }
        }

        
        private void IronBoot()
        {
                if (_timeBoot > 1f)
                {
                        _efectAyakkabı = true;
                        _IsActiveIron = !_IsActiveIron;
                        if (_IsActiveIron)
                        {
                                RB2.gravityScale = 6f;
                                BootPanel.SetActive(true);
                                Invoke("IronBootInvoke",1.5f);
                        }
                        else
                        {
                                RB2.gravityScale = 1f;
                                BootPanel.SetActive(false);
                        }
                        _timeBoot = 0;
                }
                
        }

        void IronBootInvoke()
        {
                _IsActiveIron = !_IsActiveIron;
                RB2.gravityScale = 1f;
                BootPanel.SetActive(false);
        }
}


