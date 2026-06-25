using UnityEngine;

public class ButtonPressed : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Animator wallAnim;
    [SerializeField] private float radius;
    [SerializeField] LayerMask layer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPressed()
    {
        anim.SetBool("isPressed", true);
        wallAnim.SetBool("isPressed", true);
    }

    void OnExit()
    {
        anim.SetBool("isPressed", false);
        wallAnim.SetBool("isPressed", false);
    }

    private void FixedUpdate()
    {
        OnCollision();
    }

    void OnCollision()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, layer);

        if (hit != null)
        {
            OnPressed();
            hit = null;
        }

        else
        {
            OnExit();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
