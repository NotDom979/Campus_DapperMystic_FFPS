using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class gunStats : ScriptableObject
{
	//Stats variable 
	public float shootRate;
	public int shootDist;
	public int shootDamage;
	public int ammoCount;
	public int maxAmmo;
	public float reloadTime;

	public int currentAmmoleft;
	public int maxAmmoSizeForGun;
	
	public string Tag;
	public int weaponCost;
	
	//FeedBack for the gun
	public GameObject gunModel;
	public GameObject hitEffect;
	public GameObject muzzleEffect;
	public GameObject shotPoint;
	public GameObject bullet;
	
	//Item was/wasn't picked up
	public bool purchased;
	
	//Audio Sounds (hit markers)
	public AudioClip sound;
}
