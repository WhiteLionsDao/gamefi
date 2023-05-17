using UnityEngine;

public class Contreller : MonoBehaviour
{

    Rigidbody2D rb { get; set; } // erisim belirleyici eksik

    public float Speed { get; set; } = 150f;
    public bool SagYon { get; set; } = true; // Prop kullanmiyorsan awake metodunda ilklendirme yapabilirsin
    private bool Zipla { get; set; } = true; // private public ????
    public float jump_force;
    [SerializeField] private float jetpackForce = 5f; // Jetpack itme kuvveti

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;


    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] GameObject BootPanel;

    private bool _IsActiveIron;

    [SerializeField] float fieldOfImpact;
    [SerializeField] float force;
    [SerializeField] LayerMask layerToHit;

    public GameObject firlatilacakPrefab; // Fýrlatýlacak nesnenin prefabý
    public Transform firlatmaNoktasi; // Fýrlatma noktasý
    public float firlatmaGucu = 10f; // Fýrlatma gücü


    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        BootPanel.SetActive(false);
    }

    public void Update()
    {

        if (IsGround())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {


            Firlat();
        }



        if (Input.GetKeyDown(KeyCode.W) && Zipla)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            IronBoot();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            JetPack();

        }
    }

    public void FixedUpdate()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector3(horizontal * Time.deltaTime * Speed, rb.velocity.y, 0);

       
        if (horizontal != 0)
            Rotation(horizontal);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "YER") 
        {
            Zipla = true;
        }
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jump_force)); 
        Zipla = false;
        coyoteTimeCounter = 0f;
    }

    private bool IsGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer); 
    }

    private void Rotation(float horizontal)
    {
        SagYon = horizontal is 1;
        float x = Mathf.Sign(gameObject.transform.localScale.x) == -1 && horizontal < 0 || Mathf.Sign(gameObject.transform.localScale.x) == 1 && horizontal > 0 || horizontal == 0
            ? gameObject.transform.localScale.x
            : gameObject.transform.localScale.x * -1;
        Vector3 temp = new(x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        gameObject.transform.localScale = temp;
    }

    private void IronBoot()
    {
        _IsActiveIron = !_IsActiveIron;
        if (_IsActiveIron)
        {
            rb.gravityScale = 5.81f;
            BootPanel.SetActive(true);
        }
        else
        {
            rb.gravityScale = 0.4f;
            BootPanel.SetActive(false);
        }
    }
    void JetPack()
    {
        rb.AddForce(new Vector2(0f, jetpackForce));
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }
    public GameObject firlatilanNesne;
    void Firlat()
    {
        firlatilanNesne = Instantiate(firlatilacakPrefab, firlatmaNoktasi.position, Quaternion.identity);
        Rigidbody2D rb = firlatilanNesne.GetComponent<Rigidbody2D>();
        rb.AddForce((SagYon ? Vector2.right : Vector2.left) * firlatmaGucu, ForceMode2D.Impulse);
    }

}
