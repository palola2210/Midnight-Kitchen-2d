using UnityEngine;

public class GoblinAnim : MonoBehaviour
{
    private Animator anim;

    private Goblin goblin;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        goblin = GetComponentInParent<Goblin>();
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
        if (goblin.RunAnim)
        {
            anim.SetInteger("transition", 1);
            goblin.RunAnim = false; 
        }

        if (goblin.AttackAnim)
        {
            anim.SetInteger("transition", 2);
            goblin.AttackAnim = false;
        }

        if (goblin.HitAnim)
        {
            anim.SetTrigger("hit");
            goblin.HitAnim = false;
        }

        if (goblin.DeathAnim)
        {
            anim.SetTrigger("death");
            goblin.DeathAnim = false;
        }

    }
}
