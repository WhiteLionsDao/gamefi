using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayConttroler : MonoBehaviour
{
    private PlayerManager _playerManager;
    
    public int isTocuhTheFloar;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private GameObject _gameManager;
    [SerializeField] private Rigidbody2D RB2;
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform Namlu;
    [SerializeField] private GameObject Mermi;
    [SerializeField] WhichLookLocation _whichLookLocation;
    [SerializeField] private GameObject _gameOver;
    
    private HealthBar _healthBarScript;
    private Rigidbody2D PlayerRB2;
    private float yatayEksen;
    private float dikeyEksen;
    private float _zaman;
    private float x_scale;
    
    
    private int health = 1500;  // bu artıcıan healtbardan da arttır
    private int health_defaulf;
    private void Awake()
    {
        _healthBarScript = _healthBar.GetComponent<HealthBar>();
        yatayEksen = Input.GetAxis("Horizontal");
        dikeyEksen = Input.GetAxis("Vertical");
        health_defaulf = health;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ZeminTag"))
        {
            isTocuhTheFloar = 1;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ZeminTag"))
        {
            isTocuhTheFloar = 0;
        }
    }
    private void FixedUpdate()
    {
        _zaman = _zaman + Time.deltaTime;
        if (Input.GetKey(KeyCode.P) && _zaman > 0.5)
        {
            _zaman = 0;
            MermiAteşleme();
        }
    }

    private void Update()
    {
        _whichLookLocation.RotatePlayerLocation(Player);
        _healthBarScript.health_player = health;
    }
    // ReSharper disable Unity.PerformanceAnalysis
    void MermiAteşleme()    // playerin ve Enmyin baktıgı yöne dogru mermiyi yollaman lazım.   BURRRDAASINNNNNNN.
    {
        GameObject MermiCreated;
        MermiCreated = Instantiate(Mermi, Namlu.position, transform.rotation);
        MermiCreated.GetComponent<Rigidbody2D>().velocity = new Vector2(10,MermiCreated.GetComponent<Rigidbody2D>().velocity.y);
        Destroy(MermiCreated,3f);
    }
    public void TakeDamagePlayer(int dmg,GameObject GameObject)
    {
        health -= dmg;
        if (health <= 0)
        {
            _gameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }


}
