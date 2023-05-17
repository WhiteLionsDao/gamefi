using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScripts : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Health")
        {
            Destroy(collision.gameObject);
            CanBar.can += 10;
        }
    }
}
