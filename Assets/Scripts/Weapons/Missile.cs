using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float damage;
    [SerializeField] int speed;
    [SerializeField] int destroyTime;
	[SerializeField] public float blastRadius;
	public GameObject explosionEffect;
	// Start is called before the first frame update

	enemyAi enemy;
	Vector3 pos;
	void Start()
    {
        rb.velocity = transform.forward * speed;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
			Explode();
        Destroy(gameObject, destroyTime);
          
        }
    }
	void Explode()
	{
		pos = transform.position;
		Instantiate(explosionEffect, pos, transform.rotation);
		AreaDamageForObjects(pos, blastRadius,damage);



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

				enemyHit.takeDamage(damage);


			}
			if (playerHit != null)
			{
				GameManager.instance.playerScript.takeDamage((int)damage);

			}

		}
	}
}
