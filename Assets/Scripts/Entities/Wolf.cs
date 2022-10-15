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



    bool InRadius;
    bool isAttacking;

    Vector3 playerDirection;
    float stoppingDistOrigin;
    Vector3 startPos;
    float angle;
    float speedPatrol;
    int SpeedOrig;

    // Start is called before the first frame update
    void Start()
    {

        SpeedOrig = speedChase;
        animator.SetBool("howl", true);

        grunt.pitch = 2;
        grunt.volume = .598f;
        GameManager.instance.enemyNumber++;
        currentHealth = maxHealth;
        GameManager.instance.enemyCountText.text = GameManager.instance.enemyNumber.ToString("F0");
        //healthBar.SetMaxHealth(maxHealth);

        stoppingDistOrigin = agent.stoppingDistance;
        agent.stoppingDistance = 0;

        startPos = transform.position;

        speedPatrol = agent.speed;

    }


    void Update()
    {
        if (agent.enabled)
        {

            footSteps.enabled = true;
            if (InRadius)
            {
                animator.SetBool("howl", false);
                playerDirection = GameManager.instance.player.transform.position - HeadPos.transform.position;
                angle = Vector3.Angle(playerDirection, transform.forward);

                CanSeePlayer();

            }
            else
            {

                animator.SetBool("howl", true);
            }

        }
    }

    public void takeDamage(float dmg)
    {
        currentHealth -= dmg;
        enemyHpBar.fillAmount = currentHealth / maxHealth;
        grunt.Play(1);
        if (currentHealth <= 0)
        {
            StartCoroutine(death());
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
        agent.enabled = false;
        yield return new WaitForSeconds(.35f);
        model.material.color = Color.white;
        speedChase = SpeedOrig;
        agent.enabled = true;
        agent.stoppingDistance = 0;
        agent.SetDestination(GameManager.instance.player.transform.position);
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        animator.SetTrigger("attack");
        speedChase = 0;
        yield return new WaitForSeconds(AttackRate);
        speedChase = SpeedOrig;
        isAttacking = false;
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

    IEnumerator death()
    {
        speedChase = 0;
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
            if (hit.collider.CompareTag("Player") && angle <= viewAngle)
            {
                agent.speed = speedChase;
                agent.stoppingDistance = stoppingDistOrigin;
                agent.SetDestination(GameManager.instance.player.transform.position);

                if (!isAttacking && currentHealth > 0)
                {
                    if (agent.stoppingDistance > agent.remainingDistance)
                    {
                        StartCoroutine(Attack());
                    }
                }

                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    facePlayer();
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