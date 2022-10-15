using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class enemyAi : MonoBehaviour, IDamage
{
    [Header("-----Components-----")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer model;
    [SerializeField] private Animator animator;
    [SerializeField] public AudioSource gunShot;
    [SerializeField] public AudioSource footSteps;
    [SerializeField] public AudioSource grunt;

    [Header("-----Enemy Stats-----")]
    public float maxHealth = 10;
    float currentHealth;
    public Image enemyHpBar;
    [SerializeField] int sightDistance;
    [SerializeField] int roamDist;
    [SerializeField] int viewAngle;
    [SerializeField] GameObject HeadPos;
    [SerializeField] int speedChase;
    [SerializeField] int FacePlayerSpeed;

    [Header("-----Enemy Gun Stats-----")]
    [SerializeField] float shootRate;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shotPoint;



    bool InRadius;
    bool isShooting;

    Vector3 playerDirection;
    float stoppingDistOrigin;
    Vector3 startPos;
    float angle;
    float speedPatrol;

    // Start is called before the first frame update
    void Start()
    {
        grunt.pitch = 2;
        grunt.volume = .598f;
        GameManager.instance.enemyNumber++;
        currentHealth = maxHealth;
        GameManager.instance.enemyCountText.text = GameManager.instance.enemyNumber.ToString("F0");

        stoppingDistOrigin = agent.stoppingDistance;
        agent.stoppingDistance = 0;

        startPos = transform.position;

        speedPatrol = agent.speed;
        animator.SetInteger("Status_walk", 1);
	    // Roam();
    }


    void Update()
    {
        if (agent.enabled)
        {
                animator.SetInteger("Status_walk", 1);
                footSteps.enabled = true;
            if (InRadius)
            {
                playerDirection = GameManager.instance.player.transform.position - HeadPos.transform.position;
                angle = Vector3.Angle(playerDirection, transform.forward);

                CanSeePlayer();

            }
            else if (agent.remainingDistance < 0.1f && agent.destination != GameManager.instance.player.transform.position)
            {
	            //Roam();
            }
        }
    }

    public void takeDamage(float dmg)
    {
        currentHealth -= dmg;
        enemyHpBar.fillAmount = currentHealth / maxHealth;
        grunt.Play(1);
        StartCoroutine(flashDamage());
        if (currentHealth <= 0)
        {
            StartCoroutine(death());
        }
    }

    IEnumerator flashDamage()
    {
        model.material.color = Color.red;
        agent.enabled = false;
        yield return new WaitForSeconds(.01f);
        model.material.color = Color.white;
        agent.enabled = true;
        agent.stoppingDistance = 0;
        agent.SetDestination(GameManager.instance.player.transform.position);
    }

    IEnumerator Shoot()
    {
        isShooting = true;

        Instantiate(bullet, shotPoint.transform.position, transform.rotation);

        gunShot.Play();

        yield return new WaitForSeconds(shootRate);

        gunShot.Stop();
        isShooting = false;
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
        grunt.pitch = 1;
        grunt.volume = 1;
        grunt.Play(1);
        yield return new WaitForSeconds(.32f);
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

                if (!isShooting)
                {
                    StartCoroutine(Shoot());
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


    void Roam()
    {
        agent.stoppingDistance = 0;
        agent.speed = speedPatrol;
        Vector3 randomDir = Random.insideUnitSphere * roamDist;

        randomDir += startPos;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDir, out hit, 1, 1);
        NavMeshPath path = new NavMeshPath();

        agent.CalculatePath(hit.position, path);


        agent.SetPath(path);

    }

}