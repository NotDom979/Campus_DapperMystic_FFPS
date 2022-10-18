using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
	public int amount;
	private void OnTriggerEnter(Collider other)
	{
		playerController health = other.GetComponent<playerController>();
		
		if (health)
		{
			health.AddHealth(amount);
			Destroy(gameObject);
		}
	}
}
