using UnityEngine;

public class SlimeAnim : MonoBehaviour
{
    private Animator anim;
    private Slime slime;

    void Awake()
    {
        anim = GetComponent<Animator>();
        slime = GetComponentInParent<Slime>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimController();
    }

    void AnimController()
    {
        if (slime.HitAnim)
        {
            anim.SetTrigger("hit");
            slime.HitAnim = false;
        }

        if (slime.DeathAnim)
        {
            anim.SetTrigger("death");
            slime.DeathAnim = false;
        }
    }
}
