using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MineTrigger : MonoBehaviour
{
    [SerializeField] public float blastRadius;
    public GameObject explosionEffect;


    [SerializeField] public float dmg;

    Vector3 pos;

    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }




    void Explode()
    {
        pos = transform.position;
        Instantiate(explosionEffect, pos, transform.rotation);
        AreaDamageForObjects(pos, blastRadius, dmg);


        Debug.Log("Boom");

        Destroy(gameObject);

    }

    void AreaDamageForObjects(Vector3 location, float radiusofEntity, float damage)
    {
        Collider[] objectsInRange = Physics.OverlapSphere(location, radiusofEntity);
        foreach (Collider nearbyEntities in objectsInRange)
        {
            enemyAi enemyHit = nearbyEntities.GetComponent<enemyAi>();
            playerController playerHit = nearbyEntities.GetComponent<playerController>();

            if (enemyHit != null)
            {

                enemyHit.takeDamage(dmg);


            }
            if (playerHit != null)
            {
                GameManager.instance.playerScript.takeDamage((int)dmg);

            }

        }
    }
}
