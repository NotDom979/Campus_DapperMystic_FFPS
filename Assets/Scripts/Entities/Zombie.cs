using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Zombie : enemyBase, IDamage
{
    bool isAttacking;
    //public Animator animator;

    protected override void Awake()
    {
        agent.SetDestination(target.transform.position);
        base.Awake();
    }

    public GameObject lefthand;
    public GameObject righthand;

    protected override void Update()
    {
        if (lefthand.GetComponentInChildren<Collider>().enabled == true && righthand.GetComponentInChildren<Collider>().enabled == true && !isAttacking)
        {
            lefthand.GetComponentInChildren<Collider>().enabled = false;
            righthand.GetComponentInChildren<Collider>().enabled = false;
        }
        else
        {
            lefthand.GetComponentInChildren<Collider>().enabled = true;
            righthand.GetComponentInChildren<Collider>().enabled = true;
        }

       
        base.Update();
    }

    //protected override void FindTarget()
    //{
    //    base.FindTarget();
    //    if (agent.SetDestination(target.transform.position))
    //    {
    //        if (agent.stoppingDistance > agent.remainingDistance)
    //        {
    //            if (!isAttacking)
    //            {
    //                StartCoroutine(Attack());
    //            }
    //        }
    //    }
    //}
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

        if (agent.SetDestination(target.transform.position))
        {
            if (agent.stoppingDistance > agent.remainingDistance)
            {
                if (!isAttacking)
                {
                    StartCoroutine(Attack());
                }
            }
        }
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
