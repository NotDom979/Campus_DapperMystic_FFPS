﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteEnemy : enemyBase, IDamage
{

    bool isAttacking;

	// public Animator animator;

    public int i;
    public int j;

    protected override void CanSeePlayer()
    {
        if (agent.stoppingDistance > agent.remainingDistance && angle <= viewAngle)
        {
	        //animator.SetBool("fightStance", true);
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
	    animator.SetBool("fightStance", false);
        agent.speed = 0;
        if (i == 3)
        {
            i = 0;
        }

        if (i == 0)
        {
	        animator.Play("Attack");
	        
	         animator.SetTrigger("attack1");
        }
        else if (i == 1)
        {
	        animator.SetTrigger("attack2");
	      
	        animator.Play("Attack_2");
        }
	   
	    yield return new WaitForSeconds(2);
	    //animator.Play("Blend Tree");
        i++;
        agent.speed = speedPatrol;
        isAttacking = false;
    }

    protected override IEnumerator death()
    {
        animator.SetBool("Dead",true);
	    animator.Play("Dead");
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
	        animator.Play("Hit");
        }
        else if (i == 1)
        {
	        animator.Play("Hit 2");
        }
        return base.flashDamage();
    }
}
