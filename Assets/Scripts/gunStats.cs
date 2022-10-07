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
	
	
	//FeedBack for the gun
	public GameObject gunModel;
	public GameObject hitEffect;
	public GameObject muzzleEffect;
	
	//Audio Sounds (hit markers)
	public AudioClip sound;
}
