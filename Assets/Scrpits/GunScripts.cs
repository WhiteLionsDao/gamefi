using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunScripts : MonoBehaviour
{
    [SerializeField] float offset;
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform shotPoint;
    [SerializeField] private Transform target; // Takip edilecek karakterin Transform bileþeni
    Contreller contreller = new Contreller();
    private float timeBtwShots;
    public float startTimebtwShots;

    void Update()
    {
        if (timeBtwShots <= 0) // guard kullanilabilir
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject bullet = Instantiate(Bullet, shotPoint.position, transform.rotation);
                Rigidbody2D rb = bullet. GetComponent<Rigidbody2D>();
                rb.AddForce(shotPoint.right*-1 * 10f , ForceMode2D.Impulse);
                timeBtwShots = startTimebtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }


        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        this.gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + .4f, target.transform.position.z);

    }
}
