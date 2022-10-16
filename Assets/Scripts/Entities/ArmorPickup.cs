using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPickup : MonoBehaviour
{
	public int armorAmount;
	private void OnTriggerEnter(Collider other)
	{
		playerController Armor = other.GetComponent<playerController>();
		
		
		if (Armor)
		{
			Armor.AddArmor(armorAmount);
			Destroy(gameObject);
				
		}
	}
}
