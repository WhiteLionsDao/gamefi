using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kucukMeteorScrit : MonoBehaviour
{
    [SerializeField] GameObject _kücükMeteorlar;
    [SerializeField] GameObject _meteorOluşcagıYer;
    private GameObject YeniMeteor;


    private void FixedUpdate()
    {
        _kücükMeteorlar.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
        if (_kücükMeteorlar.transform.position.x <  -40)
        {
            Destroy(_kücükMeteorlar.gameObject);
            Oluştur(_kücükMeteorlar);
        }
    }

    void Oluştur(GameObject Meteor)
    {
        YeniMeteor = Instantiate(Meteor, _meteorOluşcagıYer.transform.position, transform.rotation);
        YeniMeteor.GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, 0f);
    }
}
