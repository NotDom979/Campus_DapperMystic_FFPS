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

    protected override void CanSeePlayer()
    {
        if (agent.stoppingDistance < agent.remainingDistance)
        {
            facePlayer();
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
        Instantiate(bullet, shotPoint.transform.position, transform.rotation);
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

    public void payDay(int currency)
    {
        GameManager.instance.bankTotal += currency;
    }
}
