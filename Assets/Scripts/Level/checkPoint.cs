using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			GameManager.instance.checkPoint = GameObject.FindGameObjectWithTag("CheckPoint");
		}
	}
}
