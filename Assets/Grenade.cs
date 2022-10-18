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
		pos =transform.position;
		Instantiate(explosionEffect, pos, transform.rotation);
		AreaDamageEnemies(pos,blastRadius,dmg);


        Debug.Log("Boom");
			
		Destroy(gameObject);
		
	}
	
	void AreaDamageEnemies(Vector3 location, float radius, float damage)
	{
		Collider[] objectsInRange = Physics.OverlapSphere(location, radius);
		foreach (Collider col in objectsInRange)
		{
			enemyAi enemyHit = col.GetComponent<enemyAi>();
			if (enemyHit != null)
			{
				//// linear falloff of effect
				//float proximity = (location - enemyHit.transform.position).magnitude;
				//float effect = 1 - (proximity / radius);

				enemyHit.takeDamage(dmg);

			}
		
		}
	}
}
