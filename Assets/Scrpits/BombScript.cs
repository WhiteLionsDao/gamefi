using System.Threading.Tasks;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] float fieldOfImpact;
    [SerializeField] float force;
    [SerializeField] LayerMask layerToHit;
    private Color myColor;
    private void Awake()
    {
        _ = Patla();
    }

    void Start()
    {
        myColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    public void Explode()
    {
        
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, layerToHit);
        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
        }

        Destroy(this.gameObject);
    }

    public async Task Patla()
    {
        await Task.Delay(500).ConfigureAwait(true);
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 180);
        
        await Task.Delay(500).ConfigureAwait(true);
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        
        await Task.Delay(500).ConfigureAwait(true);
        Explode();
        Destroy(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }

}
