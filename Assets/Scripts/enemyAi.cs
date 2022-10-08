﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class enemyAi : MonoBehaviour, IDamage
{
	[Header("-----Enemy Stats-----")]
	public float maxHealth = 10;
	public float currentHealth;
	public HealthBar healthBar;
	[SerializeField] int sightDistance;

	[Header("-----Components-----")]
	[SerializeField] NavMeshAgent agent;
	[SerializeField] Renderer model;

	[Header("-----Enemy Gun Stats-----")]
	[SerializeField] float shootRate;
	[SerializeField] GameObject bullet;
	[SerializeField] GameObject shotPoint;
	
	
	
	 bool InRadius;
	bool isShooting;
	
	
	
	
	// Start is called before the first frame update
	void Start()
	{
		GameManager.instance.enemyNumber++;
		currentHealth = maxHealth;
		//healthBar.SetMaxHealth(maxHealth);
	}


	void FixedUpdate()
	{
		
		if (InRadius)
		{
			Aggro();
			
			if (!isShooting)
			{
				StartCoroutine(Shoot());
			}
		}

	}



	void Aggro()
	{
        if (agent.enabled)
        {
	        agent.SetDestination(GameManager.instance.player.transform.position);
        }
		
		
		//Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
		//rig.MovePosition(pos);
		//transform.LookAt(target);
	}





	public void takeDamage(float dmg)
	{
		currentHealth -= dmg;
		healthBar.SetHealth(currentHealth);
		StartCoroutine(flashDamage());
		if (currentHealth <= 0)
		{
			
			Destroy(gameObject);
		}
	}

	IEnumerator flashDamage()
	{
		model.material.color = Color.red;
		agent.enabled = false;
		yield return new WaitForSeconds(.01f);
		model.material.color = Color.white;
		agent.enabled = true;

	}
	
	IEnumerator Shoot()
	{
		isShooting = true;
		
		Instantiate(bullet, shotPoint.transform.position, transform.rotation);
		
		yield return new WaitForSeconds(shootRate);
		
		isShooting = false;
	}



	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			InRadius = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			InRadius = false;
		}
	}

}