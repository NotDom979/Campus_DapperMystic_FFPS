using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoost : MonoBehaviour
{
	playerController player;
	// OnTriggerEnter is called when the Collider other enters the trigger.
	void OnTriggerEnter(Collider other)
	{
		player = other.GetComponent<playerController>();
		if (other.CompareTag("Player"))
		{
			player.damage *= 2;
			Destroy(gameObject);
			StartCoroutine(Wait());
		}
	}
	IEnumerator Wait(){
		yield return new WaitForSeconds(30f);
		player.damage /= 2;
	}
}
