using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class enemyAi : MonoBehaviour, IDamage
{
	//health bars/health for enemy
	public float maxHealth = 10;
	public float currentHealth;
	public HealthBar healthBar;
	
	
	
	private bool InRadius;
	[SerializeField] NavMeshAgent agent;

	
	
	
	
	// Start is called before the first frame update
	void Start()
	{
		
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}


	void FixedUpdate()
	{
		
		if (InRadius)
		{
			Aggro();
		}

	}



	void Aggro()
	{
		
		agent.SetDestination(GameManager.instance.player.transform.position);
		
		
		//Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
		//rig.MovePosition(pos);
		//transform.LookAt(target);
	}





	public void takeDamage(float dmg)
	{
		currentHealth -= dmg;
		healthBar.SetHealth(currentHealth);
		
		if (currentHealth <= 0)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			InRadius = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			InRadius = false;
		}
	}

}