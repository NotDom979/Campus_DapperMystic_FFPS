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
	[SerializeField] public AudioSource gunShot;
	[SerializeField] public AudioSource footSteps;
	[SerializeField] public AudioSource grunt;

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
		grunt.pitch = 2;
		grunt.volume = .598f;
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
			animator.SetInteger("Status_walk", 1);
			footSteps.enabled = true;
			if (!isShooting)
			{
				StartCoroutine(Shoot());
			}

			if (agent.stoppingDistance > agent.remainingDistance)
			{
				RaycastHit hit;
				Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit);
				footSteps.enabled = false;
				animator.enabled = false;
				if (GameManager.instance.playerScript.controller.isGrounded)
				{
					gameObject.transform.LookAt(GameManager.instance.player.transform);
				}
			}
            else
			{
	            animator.enabled = true;
	            footSteps.enabled = true;
				animator.SetInteger("Status_walk", 1);
			}

			
		}
        else
		{
        	footSteps.enabled = false;
			animator.SetInteger("Status_walk", 0);
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
		grunt.Play(1);
		StartCoroutine(flashDamage());
		if (currentHealth <= 0)
		{
			StartCoroutine(death());
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

		gunShot.Play();

		yield return new WaitForSeconds(shootRate);

		gunShot.Stop();
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

	IEnumerator death()
    {
		grunt.pitch = 1;
		grunt.volume = 1;
		grunt.Play(1);
		yield return new WaitForSeconds(.32f);
		Destroy(gameObject);
		GameManager.instance.CheckEnemyTotal();
	}
}