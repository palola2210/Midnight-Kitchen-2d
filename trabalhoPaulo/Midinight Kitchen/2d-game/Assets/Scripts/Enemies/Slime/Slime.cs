using UnityEngine;

public class Slime : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]private float speed;
    [SerializeField]private float radius;
    [SerializeField]private LayerMask layer;
    [SerializeField]private Transform point;
    [SerializeField]private float health;

    private bool hitAnim;
    private bool deathAnim;

    public bool HitAnim
    {
        get { return hitAnim; }
        set { hitAnim = value; }
    }

    public bool DeathAnim
    {
        get { return deathAnim; }
        set { deathAnim = value; }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
               
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    void OnMove()
    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

        Collider2D hit = Physics2D.OverlapCircle(point.position, radius, layer);

        if (hit != null)
        {
            speed = -speed;

            if(transform.eulerAngles.y == 0)
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
            
            else
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
        }
    }

    public void OnHit()
    {
        hitAnim = true;
        health -= 0.4f;

        if (health <= 0)
        {
            speed = 0;
            deathAnim = true;
            Destroy(gameObject, 0.5f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point.position, radius);
    }
}
