using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAi : MonoBehaviour, IDamage
{
	//health bars/health for enemy
	public float maxHealth = 10;
	public float currentHealth;
	
	public HealthBar healthBar;
	
	[SerializeField] int speed;
	private bool dirRight = true;
	public Transform target;
	private bool InRadius;
	Rigidbody rig;
	Vector2 move;
	
	
	
	// Start is called before the first frame update
	void Start()
	{
		rig = GetComponent<Rigidbody>();
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
		Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
		rig.MovePosition(pos);
		transform.LookAt(target);
	}


	// Update is called once per frame
	//void Docile()
	//{


	//Move the enemy back and forth
	//if (dirRight)
	//{
	//    transform.Translate(Vector2.right * speed * Time.deltaTime);
	//}

	//else
	//{
	//    transform.Translate(-Vector2.right * speed * Time.deltaTime);
	//}

	//if (transform.position.x >= 6.0f)
	//{
	//    dirRight = false;
	//}

	//if (transform.position.x <= -6.0f)
	//{
	//    dirRight = true;
	//}


	//}


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