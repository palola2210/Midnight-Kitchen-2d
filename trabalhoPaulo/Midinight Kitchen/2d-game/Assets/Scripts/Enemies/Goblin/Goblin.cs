using UnityEngine;

public class Goblin : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float health;
    private Rigidbody2D rb;
    [SerializeField]private Transform point;
    [SerializeField] private Transform behindPoint;
    
    [SerializeField] private float maxVision;
    [SerializeField] private float stopDistance;

    [SerializeField] private bool isRight;
    [SerializeField] private bool isFront;


    private Vector2 direction;

    private bool idleAnim;
    private bool runAnim;
    private bool attackAnim;
    private bool hitAnim;
    private bool deathAnim;

    public bool IdleAnim
    {
        get { return idleAnim; }
        set { idleAnim = value; }
    }

    public bool RunAnim
    {
        get { return runAnim; }
        set { runAnim = value; }
    }

    public bool AttackAnim
    {
        get { return attackAnim; }
        set { attackAnim = value; }
    }

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
        if (isRight)
        {
            direction = Vector2.right;
            transform.eulerAngles = new Vector2(0, 0);
        }

        else
        {
            direction = Vector2.left;
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GetPlayer();
        OnMove();
    }

    void OnMove()
    {
        if (isFront)
        {
            runAnim = true;

            if (isRight)
            {
                direction = Vector2.right;
                transform.eulerAngles = new Vector2(0, 0);
                rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

            }

            else
            {
                direction = Vector2.left;
                transform.eulerAngles = new Vector2(0, 180);
                rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            }
        }
    }

    void GetPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(point.position, direction, maxVision);

        if (hit.collider != null)
        {
            if (hit.transform.CompareTag("Player"))
            {
                isFront = true;

                float distance = Vector2.Distance(transform.position, hit.transform.position);

                if (distance <= stopDistance)
                {
                    attackAnim = true;

                    isFront = false;
                    rb.linearVelocity = Vector2.zero;

                    hit.transform.GetComponent<Player>().OnHit();
                }
            }      
        }

        RaycastHit2D behindHit = Physics2D.Raycast(behindPoint.position, -direction, maxVision);

        if(behindHit.collider != null)
        {
            if(behindHit.transform.CompareTag("Player"))
            {
                isRight = !isRight;
                isFront = true;
            }        
        }
    }

    public void OnHit()
    {
        health -= 0.3f;
        hitAnim = true;

        if (health <= 0 )
        {
            speed = 0;
            deathAnim = true;
            Destroy(gameObject, 0.5f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(point.position, direction * maxVision);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(behindPoint.position, -direction * maxVision);
    }
}
