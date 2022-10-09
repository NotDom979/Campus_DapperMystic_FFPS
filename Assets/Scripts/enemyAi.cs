using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class enemyAi : MonoBehaviour, IDamage
{
	[Header("-----Components-----")]
	[SerializeField] NavMeshAgent agent;
	[SerializeField] Renderer model;
	[SerializeField] private Animator animator;

	[Header("-----Enemy Stats-----")]
	public float maxHealth = 10;
	float currentHealth;
	public Image enemyHpBar;
	[SerializeField] int sightDistance;

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
		GameManager.instance.enemyCountText.text = GameManager.instance.enemyNumber.ToString("F0");
		//healthBar.SetMaxHealth(maxHealth);
	}


	void Update()
	{
		
		if (InRadius)
		{
			Aggro();
			
			if (!isShooting)
			{
				StartCoroutine(Shoot());
			}

			if (agent.stoppingDistance > agent.remainingDistance)
			{
				animator.SetInteger("Status_walk", 0);
				if (GameManager.instance.playerScript.controller.isGrounded)
				{
					gameObject.transform.LookAt(GameManager.instance.player.transform);
				}
			}
            else
            {
				animator.SetInteger("Status_walk", 1);
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
		enemyHpBar.fillAmount = currentHealth/maxHealth;
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
			//animator.SetInteger("Status_walk", 1);
			InRadius = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			//animator.SetInteger("Status_walk", 0);
			InRadius = false;
		}
	}
	
	

}