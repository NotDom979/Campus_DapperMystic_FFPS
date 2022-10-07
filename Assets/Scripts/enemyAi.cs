using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class enemyAi : MonoBehaviour, IDamage
{
	[Header("-----Enemy Stats-----")]
	public float maxHealth = 10;
	public float currentHealth;
	public HealthBar healthBar;

	[Header("-----Components-----")]
	[SerializeField] NavMeshAgent agent;
	[SerializeField] Renderer model;

	private bool InRadius;

	
	
	
	
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