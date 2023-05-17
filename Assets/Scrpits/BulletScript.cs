using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public float lifetime; 
    private void Start()
    {
        Invoke("DestroyProjectile", lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DestroyProjectile()
    {
        //eneyme degerse siol yapicaksiniz
        
        Destroy(gameObject);
    }


}
