using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRadius : MonoBehaviour
{
	[SerializeField] GameObject enemy;
	public bool inRadius;
    // Start is called before the first frame update
    void Start()
    {
	    enemy = GameObject.FindGameObjectWithTag("enemy");
	    
    }

    // Update is called once per frame
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			//animator.SetInteger("Status_walk", 1);
			enemy.GetComponent<enemyAi>().InRadius = true;
			inRadius = true;
		}
		if (other.CompareTag("Sound"))
		{
			
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
	
			inRadius = false;
		
			
		}
		if (other.CompareTag("Sound"))
		{
			
			inRadius = false;
		}
	}
	
}
