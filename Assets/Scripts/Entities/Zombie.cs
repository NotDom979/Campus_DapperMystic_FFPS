﻿using System.Collections;
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
        if (agent.stoppingDistance > agent.remainingDistance && angle <= viewAngle)
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
        agent.speed = 1;
        animator.Play("Attack");
        attackSound.Play();
        yield return new WaitForSeconds(1);
        agent.speed = speedPatrol;
        isAttacking = false;
    }
    protected override IEnumerator death()
    {
        animator.Play("Dead");
        payDay(10);
        return base.death();
    }

    public void payDay(int currency)
    {
        GameManager.instance.bankTotal += currency;
    }
}
