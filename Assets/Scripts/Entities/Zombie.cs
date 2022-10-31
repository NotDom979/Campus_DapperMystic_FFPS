using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Zombie : enemyBase, IDamage
{
    bool isAttacking;
	//public Animator animator;

    protected override void CanSeePlayer()
    {
        if (agent.stoppingDistance > agent.remainingDistance)
        {
            if (!isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
        base.CanSeePlayer();

    }

    public IEnumerator Attack()
    {
        isAttacking = true;
        agent.speed = 0;
	    animator.Play("Attack");
	    yield return new WaitForSeconds(1);
        agent.speed = speedPatrol;
        isAttacking = false;
    }
	protected override IEnumerator death()
	{
		animator.Play("Dead");
		return base.death();
	}
	protected override IEnumerator flashDamage()
	{
		animator.Play("Dead");
		return base.death();
	}



}
