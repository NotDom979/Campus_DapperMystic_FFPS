using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{
	int HP;
	playerController player;
	
	void Start()
	{
		HP = GameManager.instance.playerScript.HP;
	}
	
	void OnTriggerEnter(Collider other)
	{
		player = other.GetComponent<playerController>();
		if (other.CompareTag("Player"))
		{
			player.HP *= 2;
			Destroy(gameObject);
			StartCoroutine(Wait());
			
			
		}
	}
	IEnumerator Wait(){
		yield return new WaitForSeconds(30f);
		player.HP = HP;
	}
	
}
