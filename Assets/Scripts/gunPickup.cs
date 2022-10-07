using System.Collections;
using System.Collections.Generic;
using UnityEngine;


	public class gunPickup : MonoBehaviour
	{
		[SerializeField] gunStats gunStat;
		
		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				
				//GameManager.instance.playerScript.gunPickup(gunStat);
				Destroy(gameObject);
			}
		}
	}

