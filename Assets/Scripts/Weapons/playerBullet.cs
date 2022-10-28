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
	playerController player;
	int dmg;
	// Start is called before the first frame update
	void Start()
	{
		rb.velocity = transform.forward * speed;
		Destroy(gameObject, destroyTime);
		player = gameObject.GetComponent<playerController>();
		dmg = player.damage;
	}
	void Update(){
		if (player.damage != dmg)
		{
			damage *= player.damage;
			dmg = player.damage;
		}
		if (player.GetComponent<Collider>().CompareTag("DamageBuff"))
		{
			damage *= 2;
		}
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
