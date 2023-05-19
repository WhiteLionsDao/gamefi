using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GunScript : MonoBehaviour
{
    [SerializeField] float offset;
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform shotPoint;
    [FormerlySerializedAs("target")] [SerializeField] private Transform player; // Takip edilecek karakterin Transform bile�eni
    // Contreller contreller = new Contreller();

    private float timeBtwShots;
    public float startTimebtwShots;

    void Update()
    {
        if (timeBtwShots <= 0) // guard kullanilabilir
        {
            if (Input.GetMouseButton(0))
            {
                GameObject bullet = Instantiate(Bullet, shotPoint.position, transform.rotation);   //  transform.rotation koymasaydık ne olurdu ?
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(shotPoint.right*-1 * 30f , ForceMode2D.Impulse);   // Impulse giderek hızlanmasınımı engelliyor?
                timeBtwShots = startTimebtwShots;
                Destroy(bullet,1f);
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }


        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        
        if (difference.x > 0)
            this.GetComponent<SpriteRenderer>().flipY = true;
        else
            this.GetComponent<SpriteRenderer>().flipY = false;
        
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        this.gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y-0.25f, player.transform.position.z);

    }
}