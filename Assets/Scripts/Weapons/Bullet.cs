using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] Rigidbody rb;
	[SerializeField] int damage;
	[SerializeField] int speed;
	[SerializeField] int destroyTime;
	// Start is called before the first frame update
	void Start()
	{
		rb.velocity = transform.forward * speed;
		Destroy(gameObject, destroyTime);
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			GameManager.instance.playerScript.takeDamage(damage);
		}
		Destroy(gameObject);
	}
   
}
