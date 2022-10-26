using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour
{
	public int healZoneAmount;
   
	private void OnTriggerEnter(Collider other)
	{
		playerController healzone = other.GetComponent<playerController>();
		
		if (healzone)
		{
			healzone.replenishHealth(healZoneAmount);
		}
		
	}
}
