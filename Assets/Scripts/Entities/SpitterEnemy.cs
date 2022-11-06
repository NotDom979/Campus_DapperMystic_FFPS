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
        target = GameObject.FindGameObjectWithTag("Target");
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
        
        base.CanSeePlayer();

        if (angle <= viewAngle)
        {
            facePlayer();
        }

    }

    public IEnumerator Attack()
    {
        isAttacking = true;
        //faceTarget();
        agent.speed = 0;
        Instantiate(bullet, shotPoint.transform.position, transform.rotation);
        animator.Play("Attack");
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
