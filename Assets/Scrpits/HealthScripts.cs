using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScripts : MonoBehaviour
{
    Contreller _contreller2 = new Contreller();
    private int _healt = 55;
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {
        _contreller2.Health = 5;
        Debug.Log($"<color=green>2_Speed: </color> " + _contreller2.Health);
        ModifyPlayerHealth(45);
        Debug.Log($"<color=yellow>3Speed: </color>" + _contreller2.Health);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Health")
        {
            Destroy(collision.gameObject);
            CanBar.can += 10;
        }
    }
    private void ModifyPlayerHealth(int newHealth)
    {
        _contreller2.Health = newHealth; // Health özelliğini değiştirelim
    }
}
