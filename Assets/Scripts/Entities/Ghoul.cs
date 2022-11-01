using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Ghoul : enemyBase, IDamage
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
    ////[SerializeField] private Animator animator;
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
    //public Animation anim;


    //[Header("-----Attack Stats-----")]
    //[SerializeField] float attackRate;
    //[SerializeField] int attackType;


    //Vector3 startPos;
    //Vector3 playerDirection;
    //Vector3 targetDirection;

    //float angle;
    //float stoppingDistOrigin;

    //bool playerSeen;
    //bool isAttacking;
    //bool isDead;
    //bool inRadius;
    //// Start is called before the first frame update
    //void Start()
    //{

    //   anim.Play("Walk");


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
    //                anim.Play("Walk");
    //            }
    //            //else if (playerSeen)
    //            //{
    //            //    facePlayer();
    //            //}
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
    //        gameObject.GetComponent<Animator>().Play("Hit");


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
    //                facePlayer();
    //                anim.Play("Run");
    //                agent.SetDestination(GameManager.instance.player.transform.position);

    //                if (agent.remainingDistance < agent.stoppingDistance)
    //                {
    //                    facePlayer();
    //                    if (!isAttacking)
    //                    {
    //                        anim.Stop("Run");
    //                        StartCoroutine(Attack());
    //                    }
    //                }
    //            }
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
    //        anim.Play("Walk");
    //        agent.stoppingDistance = 0;
    //        agent.SetDestination(target.transform.position);
    //    }
    //    if (other.CompareTag("Sound"))
    //    {
    //        //animator.SetInteger("Status_walk", 0);
    //        inRadius = false;
    //        anim.Play("Walk");
    //        agent.stoppingDistance = 0;
    //        agent.SetDestination(target.transform.position);
    //    }
    //}
    #endregion

    #region //Attack info
    //IEnumerator Attack()
    //{
    //   // attackType = 0;
    //    isAttacking = true;
    //   anim.Play("Attack1");
    //    yield return new WaitForSeconds(attackRate);
    //   // attackType++;
    //    isAttacking = false;
    //}
    #endregion

    #endregion

    public Animation anim;
    bool isAttacking;

    int i;
    // public Animator animator;

    protected override void Awake()
    {
        anim.Play("Run");
        base.Awake();
    }


    protected override void CanSeePlayer()
    {
        if (agent.stoppingDistance > agent.remainingDistance && angle <= viewAngle)
        {

            if (!isAttacking)
            {
                StartCoroutine(Attack());

            }
        }
        if (angle > viewAngle)
        {
            anim.Play("Run");
            agent.SetDestination(target.transform.position);
        }
        base.CanSeePlayer();
    }


    public IEnumerator Attack()
    {
        isAttacking = true;
        agent.speed = 0;
        if (i == 2)
        {
            i = 0;
        }

        if (i == 0)
        {
            anim.Play("Attack1");
            attackSound.Play();
        }
        else if (i == 1)
        {
            anim.Play("Attack2");
            attackSound.Play();
        }

        yield return new WaitForSeconds(2);
        i++;
        agent.speed = speedPatrol;
        isAttacking = false;
    }

    protected override IEnumerator death()
    {
        anim.Play("Death");
        payDay(25);
        return base.death();
    }

    public void payDay(int currency)
    {
        GameManager.instance.bankTotal += currency;
    }

    protected override IEnumerator flashDamage()
    {

        return base.flashDamage();
    }

}
