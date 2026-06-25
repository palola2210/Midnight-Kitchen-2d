using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    private float direction;
    [SerializeField] private float speed;
    [SerializeField] private int life;
    private int lifeInicial;

    [SerializeField] private float jumpForce;
    private bool isJumping;
    private bool doubleJump;
    public Transform point;
    public float radius;
    private float recoveryTime;



    [SerializeField] private LayerMask enemyLayer;

    private bool idleAnim;
    private bool runAnim;
    private bool jumpAnim;
    private bool doubleJumpAnim;
    private bool attackAnim;
    private bool hitAnim;
    private bool deathAnim;

    public static Player instance;

    //private PlayerSFX playerSFX;

    //private PlayerPos playerPos;

    public int Life
    {
        get { return life; }
        set { life = value; }
    }

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

    public bool JumpAnim
    {
        get { return jumpAnim; }
        set { jumpAnim = value; }
    }

    public bool DoubleJumpAnim
    {
        get { return doubleJumpAnim; }
        set { doubleJumpAnim = value; }
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

        //playerSFX = GetComponent<PlayerSFX>();

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        lifeInicial = life;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //AtualizarReferenciaPlayerPos(true);
    }

    void Update()
    {
        OnJump();
        OnAttack();
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    void OnMove()
    {
        direction = Input.GetAxis("Horizontal");

        //movendo o objeto na direção do input do jogador
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        if (direction < 0)
        {
            if (!isJumping)
            {
                runAnim = true;
            }

            transform.eulerAngles = new Vector2(0, 180);
        }

        if (direction > 0)
        {
            if (!isJumping)
            {
                runAnim = true;
            }

            transform.eulerAngles = new Vector2(0, 0);
        }

        if (direction == 0 && !isJumping && !attackAnim)
        {
            idleAnim = true;
        }
    }

    void OnJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                //playerSFX.PlaySFX(playerSFX.Jump);
                jumpAnim = true;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
                doubleJump = true;
            }

            else if (doubleJump)
            {
                //playerSFX.PlaySFX(playerSFX.Jump);
                doubleJumpAnim = true;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                doubleJump = false;
            }
        }
    }

    void OnAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            attackAnim = true;

            Collider2D hit = Physics2D.OverlapCircle(point.position, radius, enemyLayer);

            if (hit != null)
            {

                //if (hit.GetComponent<Slime>())
                //{
                //    hit.GetComponent<Slime>().OnHit();
                //}

                //if (hit.GetComponent<Goblin>())
                //{
                //    hit.GetComponent<Goblin>().OnHit();
                //}

            }

            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.33f);
        attackAnim = false;
    }

    public void OnHit()
    {
        //playerSFX.PlaySFX(playerSFX.Hit);
        life--;
        hitAnim = true;


        if (life <= 0)
        {
            deathAnim = true;
            GameController.instance.ShowGameOver();

        }
    }

    public void ResetarEstadoDoPlayer()
    {
        life = lifeInicial;

        idleAnim = true;
        runAnim = false;
        jumpAnim = false;
        doubleJumpAnim = false;
        attackAnim = false;
        hitAnim = false;
        deathAnim = false;

        isJumping = false;
        doubleJump = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

    }

    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += AoCarregarCena;
    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= AoCarregarCena;
    //}

    //private void AoCarregarCena(Scene scene, LoadSceneMode mode)
    //{
    //    AtualizarReferenciaPlayerPos(true);
    //}

    //private void AtualizarReferenciaPlayerPos(bool moverPlayerParaInicioDaCena)
    //{
    //    playerPos = FindFirstObjectByType<PlayerPos>();

    //    if (playerPos == null)
    //    {
    //        Debug.LogWarning("Nenhum PlayerPos foi encontrado na cena: " + SceneManager.GetActiveScene().name);
    //        return;
    //    }

    //    if (moverPlayerParaInicioDaCena)
    //    {
    //        playerPos.Checkpoint();
    //    }
    //}

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point.position, radius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isJumping = false;
          
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            OnHit();
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            //playerSFX.PlaySFX(playerSFX.Coin);
            //collision.GetComponent<Animator>().SetTrigger("pick");
            GameController.instance.GetCoin();
            Destroy(collision.gameObject, 0.25f);
        }

        if (collision.gameObject.CompareTag("Pasta"))
        {
            //playerSFX.PlaySFX(playerSFX.Coin);
            //collision.GetComponent<Animator>().SetTrigger("destroy");
            GameController.instance.GetPasta();
            Destroy(collision.gameObject, 0.25f);
        }

        //if (collision.gameObject.layer == 10)
        //{
        //    if (playerPos == null)
        //    {
        //        AtualizarReferenciaPlayerPos(false);
        //    }

        //    if (playerPos != null)
        //    {
        //        playerPos.Checkpoint();
        //    }
        //}

    }
}
