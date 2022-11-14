using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteEnemy : enemyBase, IDamage
{

    bool isAttacking;

    // public Animator animator;

    public int i;
    public int j;

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
        RaycastHit hit;
        if (Physics.Raycast(HeadPos.transform.position, playerDirection, out hit, sightDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
               
                agent.SetDestination(GameManager.instance.player.transform.position);
            }
        }
    }

    public IEnumerator Attack()
    {
        isAttacking = true;
        lefthand.GetComponentInChildren<Collider>().enabled = true;
        righthand.GetComponentInChildren<Collider>().enabled = true;
        agent.speed = 0;

        if (i == 2)
        {
            i = 0;
        }

        if (i == 0)
        {


            animator.SetTrigger("attack1");
            attackSound.Play();
            yield return new WaitForSeconds(.5f);
            agent.speed = speedChase;
        }
        else if (i == 1)
        {

            animator.SetTrigger("attack2");
            attackSound.Play();
            yield return new WaitForSeconds(.5f);
            agent.speed = speedChase;

        }
        yield return new WaitForSeconds(1);
        agent.speed = speedChase;
        isAttacking = false;
        i++;

    }

    protected override IEnumerator death()
    {
        animator.SetBool("Dead", true);
        animator.Play("Dead");
        payDay(30);
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
