﻿using System.Collections;
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
	public string Tag;
	
	//FeedBack for the gun
	public GameObject gunModel;
	public GameObject hitEffect;
	public GameObject muzzleEffect;
	public GameObject shotPoint;
	
	//Audio Sounds (hit markers)
	public AudioClip sound;
}
