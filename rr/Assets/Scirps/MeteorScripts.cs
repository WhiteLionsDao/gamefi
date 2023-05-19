using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScripts : MonoBehaviour
{
    [SerializeField] GameObject _büyükMeteorlar;
    [SerializeField] GameObject _meteorOluşcagıYer;
    private GameObject YeniMeteor;

    private void FixedUpdate()
    {
        _büyükMeteorlar.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.2f, 0.3f);
        if (_büyükMeteorlar.transform.position.x <  -55)
        {
            Destroy(_büyükMeteorlar.gameObject);
            Oluştur(_büyükMeteorlar);
        }
    }

    void Oluştur(GameObject Meteor)
    {
        YeniMeteor = Instantiate(Meteor, _meteorOluşcagıYer.transform.position, transform.rotation);
        YeniMeteor.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.2f, 0.2f);
    }
}
