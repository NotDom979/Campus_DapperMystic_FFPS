using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Wolf : MonoBehaviour, IDamage
{
    [Header("-----Components-----")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer model;
    [SerializeField] private Animator animator;
    [SerializeField] public AudioSource footSteps;
    [SerializeField] public AudioSource grunt;
    [SerializeField] public AudioSource attack;
    [SerializeField] public AudioSource shot;
    [SerializeField] public AudioSource shot2;
    [SerializeField] public AudioSource Howl;

    [Header("-----Enemy Stats-----")]
    public float maxHealth = 10;
    float currentHealth;
    public Image enemyHpBar;
    [SerializeField] int sightDistance;
    [SerializeField] int viewAngle;
    [SerializeField] GameObject HeadPos;
    [SerializeField] int speedChase;
    [SerializeField] int FacePlayerSpeed;
    [Header("-----Enemy Gun Stats-----")]
    [SerializeField] float AttackRate;
    [SerializeField] GameObject AttackPoint;
    [SerializeField] float ShootRate;
    [SerializeField] GameObject LaunchPoint;
    [SerializeField] GameObject Projectile1;
    [SerializeField] GameObject Projectile2;
    



    bool InRadius;
    bool isAttacking;
    bool isShooting;

    bool stage1;
    bool stage2;

    Vector3 playerDirection;
    float stoppingDistOrigin;
    int speedOrig;
    Vector3 startPos;
    float angle;

    // Start is called before the first frame update
    void Start()
    {
        stage1 = true;
        stage2 = false;
        footSteps.enabled = false;

        speedChase = 10;
        speedOrig = speedChase;
        animator.SetBool("howl", true);
        Howl.Play();
        grunt.pitch = 2;
        grunt.volume = .598f;
        GameManager.instance.enemyNumber++;
        currentHealth = maxHealth;
        GameManager.instance.enemyCountText.text = GameManager.instance.enemyNumber.ToString("F0");
        //healthBar.SetMaxHealth(maxHealth);

        agent.speed = speedOrig;
        stoppingDistOrigin = agent.stoppingDistance;
        agent.stoppingDistance = 0;

        startPos = transform.position;


    }

    void Update()
    {

        if (agent.enabled)
        {

            if (InRadius)
            {
                playerDirection = GameManager.instance.player.transform.position - HeadPos.transform.position;
                angle = Vector3.Angle(playerDirection, transform.forward);

                CanSeePlayer();
                animator.SetBool("howl", false);
                footSteps.enabled = true;

            }

        }
    }

    public void takeDamage(float dmg)
    {
        currentHealth -= dmg;
        enemyHpBar.fillAmount = currentHealth / maxHealth;
        grunt.pitch = 2;
        grunt.Play(1);
        if (currentHealth <= 0)
        {
            if (stage1 && !stage2)
            {
                StartCoroutine(Round2());
            }
            else if (stage2 && !stage1)
            {
                agent.speed = 0;
                agent.enabled = false;
                StartCoroutine(death());
            }
        }
        else
        {
            animator.SetTrigger("damage");
            StartCoroutine(flashDamage());

        }
    }





    IEnumerator flashDamage()
    {
        model.material.color = Color.red;
        agent.speed = 0;
        yield return new WaitForSeconds(.5f);
        model.material.color = Color.white;
        agent.speed = speedOrig;
        agent.stoppingDistance = 0;
        agent.SetDestination(GameManager.instance.player.transform.position);
    }

    IEnumerator Attack()
    {
        if (stage1)
        {
            isAttacking = true;
            agent.speed = 0;
            attack.Play();
            animator.SetBool("attack", true);
            yield return new WaitForSeconds(AttackRate);
            animator.SetBool("attack", false);
            attack.Stop();
            agent.speed = speedOrig;
            isAttacking = false;
        }
        else if (stage2 && !stage1)
        {
            isAttacking = true;
            agent.speed = 0;
            attack.Play();
            animator.SetBool("attack", true);
            yield return new WaitForSeconds(AttackRate);
            animator.SetBool("attack", false);
            attack.Stop();
            agent.speed = 25;
            isAttacking = false;
        }
    }

    IEnumerator Shoot()
    {
        if (stage1)
        {
            isShooting = true;

            for (int i = 0; i < 3; i++)
            {
                shot.Play();
                Instantiate(Projectile1, LaunchPoint.transform.position, transform.rotation);
                yield return new WaitForSeconds(.3f);
                shot.Stop();
            }

            yield return new WaitForSeconds(ShootRate);

            isShooting = false;
        }
        else if (stage2 && !stage1)
        {
            isShooting = true;

            for (int i = 0; i < 10; i++)
            {
                shot2.Play();
                Instantiate(Projectile2, LaunchPoint.transform.position, transform.rotation);
                yield return new WaitForSeconds(.1f);
                shot2.Play();
            }

            yield return new WaitForSeconds(ShootRate);

            isShooting = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            //animator.SetInteger("Status_walk", 1);
            InRadius = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //animator.SetInteger("Status_walk", 0);
            InRadius = false;
            agent.stoppingDistance = 0;

        }
    }

    IEnumerator Round2()
    {
        stage1 = false;
        agent.speed = 0;
        animator.SetBool("Dead", true);
        speedChase = 0;
        grunt.pitch = 1;
        grunt.volume = 1;
        grunt.Play(1);
        yield return new WaitForSeconds(4);
        animator.SetBool("Dead", false);
        yield return new WaitForSeconds(2);
        currentHealth = 125;
        maxHealth = 125;
        enemyHpBar.fillAmount = maxHealth;
        agent.speed = 20;
        speedOrig = 20;
        stage2 = true;

        agent.SetDestination(GameManager.instance.player.transform.position);
    }
    IEnumerator death()
    {

        agent.speed = 0;
        animator.SetBool("attack", false);
        animator.SetBool("Dead", true);
        agent.enabled = false;
        grunt.pitch = 1;
        grunt.volume = 1;
        grunt.Play(1);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        GameManager.instance.CheckEnemyTotal();
    }

    void CanSeePlayer()
    {

        RaycastHit hit;

        if (Physics.Raycast(HeadPos.transform.position, playerDirection, out hit, sightDistance))
        {
            Debug.DrawRay(HeadPos.transform.position, playerDirection);
            Debug.Log(angle);
            if (hit.collider.CompareTag("Player"))
            {
                facePlayer();

                agent.stoppingDistance = stoppingDistOrigin;
                agent.SetDestination(GameManager.instance.player.transform.position);


                if (currentHealth > 1)
                {
                    if (!isAttacking)
                    {
                        if (agent.stoppingDistance > agent.remainingDistance)
                        {
                            StartCoroutine(Attack());
                        }

                    }

                    if (!isShooting)
                    {
                        if (agent.remainingDistance > agent.stoppingDistance)
                        {

                            StartCoroutine(Shoot());
                        }

                    }
                }





            }

        }


    }


    void facePlayer()
    {
        playerDirection.y = 0;
        Quaternion rotation = Quaternion.LookRotation(playerDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * FacePlayerSpeed);
    }




}