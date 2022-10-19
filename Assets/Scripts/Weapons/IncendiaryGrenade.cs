using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncendiaryGrenade : MonoBehaviour
{
    [SerializeField] public float delay = 4f;
    [SerializeField] public float blastRadius;
    public GameObject explosionEffect;
    public Collider sphereCollider;

    float countDown;
    bool isExplode = false;

    [SerializeField] public float dmg;

    Vector3 pos;


    void Start()
    {
        sphereCollider.enabled = false;

        countDown = delay;

    }

    void Update()
    {

        countDown -= Time.deltaTime;
        if (countDown <= 0f && !isExplode)
        {
            sphereCollider.enabled = true;
            Explode();
            isExplode = true;
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        pos = transform.position;
        Instantiate(explosionEffect, pos, transform.rotation);

        AreaDamageEnemies(pos, blastRadius, dmg);

        Debug.Log("Boom");


    }

    void AreaDamageEnemies(Vector3 location, float radiusofEntity, float damage)
    {
        Collider[] objectsInRange = Physics.OverlapSphere(location, radiusofEntity);
        foreach (Collider nearbyEntities in objectsInRange)
        {
            enemyAi enemyHit = nearbyEntities.GetComponent<enemyAi>();
            playerController playerHit = nearbyEntities.GetComponent<playerController>();



            StatusManager burn = nearbyEntities.GetComponent<StatusManager>();

            if (enemyHit != null)
            {
                if (enemyHit.GetComponent<StatusManager>() != null)
                {

                }
                    enemyHit.takeDamage(dmg);
                    GameManager.instance.playerScript.takeDamage((int)dmg);

            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<StatusManager>() != null)
        {
            other.GetComponent<StatusManager>().ApplyBurn(6);
        }
    }

   
}

