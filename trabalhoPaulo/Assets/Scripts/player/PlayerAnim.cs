using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Player player;
    private Animator anim;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        anim = GetComponent<Animator>();
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
        if (player.IdleAnim)
        {
            anim.SetInteger("base", 0);
            player.IdleAnim = false;
        }

        if (player.RunAnim)
        {
            anim.SetInteger("base", 1);
            player.RunAnim = false;
        }

        if (player.JumpAnim)
        {
            anim.SetInteger("base", 4);
            player.JumpAnim = false;
        }

        if (player.DoubleJumpAnim)
        {
            anim.SetInteger("base", 2);
            player.DoubleJumpAnim = false;
        }

        if (player.AttackAnim)
        {
            anim.SetInteger("transition", 4);
        }

        //if (player.HitAnim)
        //{
        //    anim.SetTrigger("hit");
        //    player.HitAnim = false;
        //}

        //if (player.DeathAnim)
        //{
        //    anim.SetTrigger("death");
        //    player.DeathAnim = false;
        //}
    }
}