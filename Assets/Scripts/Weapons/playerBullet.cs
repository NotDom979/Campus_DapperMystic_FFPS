using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
	[SerializeField] Rigidbody rb;
	[SerializeField] int damage;
	[SerializeField] int speed;
	[SerializeField] int destroyTime;
	GameObject enemy;
	// Start is called before the first frame update
	void Start()
	{
		rb.velocity = transform.forward * speed;
		Destroy(gameObject, destroyTime);
	}


	private void OnTriggerEnter(Collider other)
	{
		
		if (other.CompareTag("enemy"))
		{
			other.gameObject.GetComponent<IDamage>().takeDamage(damage);
			//other.GetComponent<IDamage>().takeDamage(damage);
		}
		Destroy(gameObject,destroyTime);
	}
   
}
