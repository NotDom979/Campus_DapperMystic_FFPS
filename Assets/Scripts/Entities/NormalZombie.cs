using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NormalZombie : enemyBase
{
    #region //previous code
    //[Header("-----Health-----")]
    //public Image enemyHpBar;
    //public float maxHealth;
    //public float currentHealth;

    ////[Header("-----Sounds-----")]
    ////[SerializeField] public AudioSource grunt;
    ////[SerializeField] public AudioSource footSteps;
    ////[SerializeField] public AudioSource attack;

    //[Header("-----Nav Stats-----")]
    //[SerializeField] NavMeshAgent agent;
    //[SerializeField] private Animator animator;
    //[SerializeField] Renderer model;
    //[SerializeField] int speedChase;
    //[SerializeField] float speedPatrol;
    //[SerializeField] GameObject target;

    //[Header("-----General Stats-----")]
    //[SerializeField] GameObject HeadPos;
    //[SerializeField] int sightDistance;
    //[SerializeField] int roamDist;
    //[SerializeField] int viewAngle;
    //[SerializeField] int FacePlayerSpeed;


    //[Header("-----Attack Stats-----")]
    //[SerializeField] float shotRate;


    //Vector3 startPos;
    //Vector3 playerDirection;
    //Vector3 targetDirection;

    //float angle;
    //float stoppingDistOrigin;
    //float newAnimSpeed;

    //bool playerSeen;
    //bool isShooting;
    //bool isDead;
    //bool inRadius;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    // attack.enabled = false;
    //    playerSeen = false;



    //    currentHealth = maxHealth;

    //    GameManager.instance.enemyNumber++;
    //    GameManager.instance.enemyCountText.text = GameManager.instance.enemyNumber.ToString("F0");

    //    stoppingDistOrigin = agent.stoppingDistance;
    //    agent.stoppingDistance = 0;

    //    startPos = transform.position;
    //    speedPatrol = agent.speed;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (GameManager.instance.pauseMenu.activeSelf == false)
    //    {
    //        if (agent.enabled)
    //        {

    //            if (inRadius)
    //            {

    //                playerDirection = GameManager.instance.player.transform.position - HeadPos.transform.position;
    //                angle = Vector3.Angle(playerDirection, transform.forward);

    //                CanSeePlayer();

    //            }
    //            else
    //            {
    //                agent.stoppingDistance = stoppingDistOrigin;
    //                faceTarget();
    //                agent.SetDestination(target.transform.position);
    //            }

    //        }
    //    }
    //}

    #region //Damage info

    //public void takeDamage(float dmg)
    //{
    //    currentHealth -= dmg;
    //    enemyHpBar.fillAmount = currentHealth / maxHealth;
    //    //grunt.Play();
    //    if (currentHealth <= 0)
    //    {
    //        StartCoroutine(death());
    //    }
    //    else
    //        animator.SetTrigger("hurt");


    //    agent.SetDestination(GameManager.instance.player.transform.position);
    //    StartCoroutine(flashDamage());
    //}


    //IEnumerator flashDamage()
    //{
    //    model.material.color = Color.red;
    //    agent.speed = 0;
    //    yield return new WaitForSeconds(.5f);
    //    model.material.color = Color.white;
    //    agent.speed = speedPatrol;
    //    agent.stoppingDistance = 0;

    //}

    //IEnumerator death()
    //{

    //    agent.speed = 0;
    //    animator.SetBool("death", true);
    //    yield return new WaitForSeconds(2f);
    //    Destroy(gameObject);
    //    GameManager.instance.CheckEnemyTotal();
    //}
    #endregion

    #region //Movement info
    //void CanSeePlayer()
    //{
    //    RaycastHit hit;

    //    if (Physics.Raycast(HeadPos.transform.position, playerDirection, out hit, sightDistance))
    //    {
    //        Debug.DrawRay(HeadPos.transform.position, playerDirection);
    //        Debug.Log(angle);
    //        if (hit.collider.CompareTag("Player"))
    //        {
    //            playerSeen = true;
    //            if (angle <= viewAngle)
    //            {
    //                agent.speed = speedChase;
    //                agent.stoppingDistance = stoppingDistOrigin;
    //                agent.SetDestination(GameManager.instance.player.transform.position);

    //                if (isShooting)
    //                {
    //                    StartCoroutine(Shoot());
    //                }

    //                if (agent.remainingDistance < agent.stoppingDistance)
    //                {
    //                    facePlayer();
    //                }

    //            }
    //        }
    //        else
    //        {
    //            playerSeen = false;
    //        }

    //    }
    //}

    //void facePlayer()
    //{
    //    playerDirection.y = 0;
    //    Quaternion rotation = Quaternion.LookRotation(playerDirection);
    //    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * FacePlayerSpeed);
    //}


    //void faceTarget()
    //{
    //    Quaternion rotation = Quaternion.LookRotation(targetDirection);
    //    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * FacePlayerSpeed);

    //}
    #endregion

    #region //OnTriggers
    //private void OnTriggerEnter(Collider other)
    //{

    //    if (other.CompareTag("Player"))
    //    {
    //        //animator.SetInteger("Status_walk", 1);
    //        inRadius = true;
    //        //facePlayer();
    //        //agent.SetDestination(GameManager.instance.player.transform.position);
    //    }
    //    if (other.CompareTag("Sound"))
    //    {
    //        //animator.SetInteger("Status_walk", 1);
    //        inRadius = true;
    //        facePlayer();
    //        agent.SetDestination(GameManager.instance.player.transform.position);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        //animator.SetInteger("Status_walk", 0);
    //        inRadius = false;
    //        animator.speed = newAnimSpeed;
    //        agent.stoppingDistance = 0;
    //        faceTarget();
    //        agent.SetDestination(target.transform.position);
    //    }
    //    if (other.CompareTag("Sound"))
    //    {
    //        //animator.SetInteger("Status_walk", 0);
    //        inRadius = false;
    //        animator.speed = newAnimSpeed;
    //        agent.stoppingDistance = 0;
    //        faceTarget();
    //        agent.SetDestination(target.transform.position);
    //    }
    //}
    #endregion

    #region //Attack info

    //IEnumerator Shoot()
    //{
    //    isShooting = true;
    //    agent.speed = 0;
    //    animator.SetTrigger("attack");
    //    //Instantiate(bullet, shotPoint.transform.position, transform.rotation);
    //    //gunShot.enabled = true;
    //    //Muzzle();     
    //    //gunShot.Play();
    //    yield return new WaitForSeconds(shotRate);
    //    //gunShot.Stop();
    //    agent.speed = speedChase;
    //    isShooting = false;

    //}
    #endregion

    #endregion

    bool isAttacking;
    public Animator animator;


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
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(1);
        agent.speed = speedPatrol;
        isAttacking = false;
    }



}
