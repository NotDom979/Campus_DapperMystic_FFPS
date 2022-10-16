using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
	[SerializeField]public float delay = 4f;
	[SerializeField]public float blastRadius= 4f;
	[SerializeField] public GameObject explosion;
	float countDown;
	bool didExplode = false;
	
	void Start(){
		
		countDown = delay;
		
	}
	
	void Update(){
		
		countDown-= Time.deltaTime;
		if (countDown <= 0f && !didExplode)
		{
			Explode();
			didExplode = true;
		}
	}
	
	void Explode()
	{
		Instantiate(explosion,transform.position, transform.rotation);
		//getting near by objects so when explosion goes off
		
		//gotta find explosion effect
		Collider[] colliders = Physics.OverlapSphere(explosion.transform.position,blastRadius);
		Destroy(gameObject);
		foreach (Collider nearGrenade in colliders)
		{
			nearGrenade.GetComponents<CapsuleCollider>();
			if (colliders != null)
			{
					//add Damage
				
			}
			
		}
		Debug.Log("Boom");
	}
}
