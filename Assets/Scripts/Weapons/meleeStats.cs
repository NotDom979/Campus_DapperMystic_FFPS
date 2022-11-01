using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class meleeStats : ScriptableObject
{
	//Stats variable 
	public float shootRate;
	public int shootDamage;
	
	public string Tag;
	public int weaponCost;
	
	//FeedBack for the gun
	public GameObject gunModel;
	public GameObject hitEffect;
	
	//Item was/wasn't picked up
	public bool purchased;
	
	//Audio Sounds (hit markers)
	public AudioClip sound;
}
