using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBoost : MonoBehaviour
{
	int armor;
	playerController player;
	
	void Start()
	{
		armor = GameManager.instance.playerScript.Armor;
	}
	
	void OnTriggerEnter(Collider other)
	{
		player = other.GetComponent<playerController>();
		if (other.CompareTag("Player"))
		{
			player.Armor *= 2;
			Destroy(gameObject);
			StartCoroutine(Wait());
			
			
		}
	}
	IEnumerator Wait(){
		yield return new WaitForSeconds(30f);
		player.Armor = armor;
	}
	
}
