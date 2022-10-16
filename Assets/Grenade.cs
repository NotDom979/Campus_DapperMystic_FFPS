using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
	[SerializeField]public float delay = 4f;
	[SerializeField]public float d= 4f;
	
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
		GameObject explosion = Instantiate(explosion, explosion.transform.position, transform.rotation);
		//getting near by objects so when explosion goes off
		//add damage and force
		//gotta find explosion effect
		Destroy(explosion);
		
		Debug.Log("Boom");
	}
}
