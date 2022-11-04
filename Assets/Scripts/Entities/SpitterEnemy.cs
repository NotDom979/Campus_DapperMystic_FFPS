using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SpitterEnemy : enemyBase, IDamage
{
    bool isAttacking;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shotPoint;
    //public Animator animator;


    protected override void Awake()
    {
        base.Awake();
        agent.SetDestination(target.transform.position);
        agent.stoppingDistance = 10;
    }


    protected override void Update()
    {

    
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
        //if (agent.stoppingDistance < agent.remainingDistance)
        //{
        //    facePlayer();
        //    if (!isAttacking)
        //    {
        //        StartCoroutine(Attack());
        //    }
        //}
        base.CanSeePlayer();

        if (angle > viewAngle)
        {
            faceTarget();
            agent.SetDestination(target.transform.position);
        }

    }

    public IEnumerator Attack()
    {
        isAttacking = true;
        agent.speed = 0;
        animator.Play("Attack");
        Instantiate(bullet, shotPoint.transform.position, transform.rotation);
        attackSound.Play();
        yield return new WaitForSeconds(1.5f);
        agent.speed = speedPatrol;
        isAttacking = false;
    }
    protected override IEnumerator death()
    {
        animator.Play("Dead");
        payDay(15);
        return base.death();
    }

   
}
