using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
	[SerializeField]public float delay = 4f;
	[SerializeField]public float blastRadius;
	public GameObject explosionEffect;
	
	float countDown;
	bool isExplode = false;
	
	[SerializeField] public float dmg;
	
	Vector3 pos;
	
	
	void Start(){
		countDown = delay;
		
	}
	
	void Update(){
		
		countDown-= Time.deltaTime;
		if (countDown <= 0f && !isExplode)
		{
			Explode();
			isExplode = true;
		}
	}
	
	void Explode()
	{
		pos = transform.position;
		Instantiate(explosionEffect, pos, transform.rotation);
		AreaDamageEnemies(pos,blastRadius,dmg);


        Debug.Log("Boom");
			
		Destroy(gameObject);
		
	}
	
	void AreaDamageEnemies(Vector3 location, float radiusofEntity, float damage)
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
