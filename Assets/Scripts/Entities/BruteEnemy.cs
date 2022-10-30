using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteEnemy : enemyBase
{

    bool isAttacking;

    public Animator animator;

    public int i;
    public int j;

    protected override void CanSeePlayer()
    {
        if (agent.stoppingDistance > agent.remainingDistance && angle <= viewAngle)
        {
            animator.SetBool("fightStance", true);
            if (!isAttacking)
            {
               StartCoroutine(Attack());
            }
        }
        else
        {
            animator.SetBool("fighStance", false);

        }
        base.CanSeePlayer();

    }


    public IEnumerator Attack()
    {
        isAttacking = true;
        agent.speed = 0;
        if (i == 3)
        {
            i = 0;
        }

        if (i == 0)
        {
            animator.SetTrigger("attack1");
        }
        else if (i == 1)
        {
            animator.SetTrigger("attack2");
        }
        else if (i ==2)
        {
            animator.SetTrigger("attack3");
        }
        yield return new WaitForSeconds(2);
        i++;
        agent.speed = speedPatrol;
        isAttacking = false;
    }

    protected override IEnumerator death()
    {
        animator.SetBool("Dead", true);
        return base.death();
    }

    protected override IEnumerator flashDamage()
    {
        if (i == 2)
        {
            i = 0;
        }

        if (i == 0)
        {
            animator.SetTrigger("hurt1");
        }
        else if (i == 1)
        {
            animator.SetTrigger("hurt2");
        }
        return base.flashDamage();
    }
}
