using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Zombie : enemyBase, IDamage
{
    bool isAttacking;
    //public Animator animator;

    public GameObject lefthand;
    public GameObject righthand;


    protected override void Awake()
    {
        base.Awake();
        agent.SetDestination(target.transform.position);
        lefthand.GetComponentInChildren<Collider>().enabled = false;
        righthand.GetComponentInChildren<Collider>().enabled = false;
    }
    protected override void Update()
    {
        if (!isAttacking)
        {
            lefthand.GetComponentInChildren<Collider>().enabled = false;
            righthand.GetComponentInChildren<Collider>().enabled = false;
        }

        if (agent.stoppingDistance > agent.remainingDistance)
        {
            if (!isAttacking)
            {
                StartCoroutine(Attack());
            }
        }


        base.Update();
    }

    protected override void CanSeePlayer()
    {
        base.CanSeePlayer();

        if (angle > viewAngle)
        {
            agent.SetDestination(target.transform.position);
        }
    }

    public IEnumerator Attack()
    {
        isAttacking = true;
        lefthand.GetComponentInChildren<Collider>().enabled = true;
        righthand.GetComponentInChildren<Collider>().enabled = true;
        agent.speed = 1;
        animator.Play("Attack");
        attackSound.Play();
        yield return new WaitForSeconds(1);
        agent.speed = speedPatrol;
        isAttacking = false;
    }
    protected override IEnumerator death()
    {
        animator.SetBool("Dead", true);
        animator.Play("Dead");
        payDay(10);
        return base.death();
        
    }

    protected override IEnumerator flashDamage()
    {

        animator.Play("hit");
        return base.flashDamage();
    }

}
