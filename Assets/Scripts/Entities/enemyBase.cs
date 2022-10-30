using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemyBase : MonoBehaviour
{
    [SerializeField] public GameObject EnemyCanvas;
    [SerializeField] public GameObject Detector;
    [SerializeField] GameObject HeadPos;
    [SerializeField] public GameObject target;

    [SerializeField] int sightDistance;
    [SerializeField] int roamDist;
    [SerializeField] public int viewAngle;
    [SerializeField] public int speedChase;
    [SerializeField] int FacePlayerSpeed;

    [SerializeField] public NavMeshAgent agent;
    [SerializeField] Renderer model;

    public Image enemyHpBar;
    public float maxHealth;
    public float currentHealth;

    Vector3 startPos;
    Vector3 targetDirection;
    Vector3 playerDirection;

    float stoppingDistOrigin;
    public float angle;
    public float speedPatrol;

    public bool InRadius;
    bool playerSeen;
    // Start is called before the first frame update
    virtual protected void Start()
    {
        EnemyCanvas = GameObject.FindGameObjectWithTag("EnemyCanvas");
        GameManager.instance.enemyNumber++;
        GameManager.instance.enemyCountText.text = GameManager.instance.enemyNumber.ToString("F0");
        playerSeen = false;
        stoppingDistOrigin = agent.stoppingDistance;
        agent.stoppingDistance = 0;

        startPos = transform.position;

        speedPatrol = agent.speed;
        //Roam();
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if (GameManager.instance.pauseMenu.activeSelf == false)
        {
            if (agent.enabled)
            {
                Detection();
                //animator.SetFloat("Speed", Mathf.Lerp(animator.GetFloat("Speed"), agent.velocity.normalized.magnitude, Time.deltaTime * 3));
                if (InRadius)
                {
                    playerDirection = GameManager.instance.player.transform.position - HeadPos.transform.position;
                    angle = Vector3.Angle(playerDirection, transform.forward);

                    CanSeePlayer();

                }
                else
                {
                    agent.stoppingDistance = stoppingDistOrigin;
                    agent.SetDestination(target.transform.position);
                }

            }
        }

    }
    virtual protected void Detection()
    {
        if (gameObject.GetComponentInChildren<DetectionRadius>().inRadius == true)
        {
            InRadius = true;
            //facePlayer();
        }
        else
        {
            InRadius = false;
            agent.stoppingDistance = 0;
        }
    }

    public void faceTarget()
    {
        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * FacePlayerSpeed);

    }

    public void facePlayer()
    {
        playerDirection.y = 0;
        Quaternion rotation = Quaternion.LookRotation(playerDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * FacePlayerSpeed);
    }
    public void takeDamage(float dmg)
    {
        currentHealth -= dmg;
        enemyHpBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            StartCoroutine(death());
        }

        agent.SetDestination(GameManager.instance.player.transform.position);
        StartCoroutine(flashDamage());
    }
    virtual protected IEnumerator death()
    {
        EnemyCanvas.SetActive(false);
        agent.speed = 0;
        agent.enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        GameManager.instance.CheckEnemyTotal();
    }
    virtual protected void Roam()
    {
        agent.stoppingDistance = 0;
        agent.speed = speedPatrol;

        Vector3 randomDir = Random.insideUnitSphere * roamDist;
        randomDir += startPos;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDir, out hit, .5f, 1);
        NavMeshPath path = new NavMeshPath();
        if (hit.hit == true)
        {
            if (hit.position != null)
            {
                agent.CalculatePath(hit.position, path);
            }
        }
        agent.SetPath(path);
    }
    virtual protected void CanSeePlayer()
    {
        RaycastHit hit;

        if (Physics.Raycast(HeadPos.transform.position, playerDirection, out hit, sightDistance))
        {
            Debug.DrawRay(HeadPos.transform.position, playerDirection);
            Debug.Log(angle);
            if (hit.collider.CompareTag("Player"))
            {
                playerSeen = true;
                if (angle <= viewAngle)
                {

                    agent.speed = speedChase;
                    agent.stoppingDistance = stoppingDistOrigin;
                    agent.SetDestination(GameManager.instance.player.transform.position);





                }

            }
            else
            {
                playerSeen = false;
            }


        }


    }

    virtual protected IEnumerator flashDamage()
    {
        model.material.color = Color.red;
        agent.speed = 0;
        yield return new WaitForSeconds(.5f);
        model.material.color = Color.white;
        agent.speed = speedPatrol;
        agent.stoppingDistance = 0;
    }
}
