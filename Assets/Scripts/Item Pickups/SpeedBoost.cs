using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
	float speed;
	playerController player;
	
	void Start()
	{
		speed = GameManager.instance.playerScript.playerSpeed;
	}
	
	void OnTriggerEnter(Collider other)
	{
	player = other.GetComponent<playerController>();
		if (other.CompareTag("Player"))
		{
			player.playerSpeed *= 2;
			StartCoroutine(Wait());
			
			
		}
	}
	IEnumerator Wait(){
		
		yield return new WaitForSeconds(3f);
		player.playerSpeed = speed;	
			Destroy(gameObject);
		 
		
	}
	
}
