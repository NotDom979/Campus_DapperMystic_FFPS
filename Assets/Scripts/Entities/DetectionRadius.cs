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
			inRadius = true;
		}
		if (other.CompareTag("Sound"))
		{
			//animator.SetInteger("Status_walk", 1);
			//	gameObject.GetComponent<enemyAi>().InRadius = true;
			//gameObject.GetComponent<enemyAi>().facePlayer();
			//gameObject.GetComponent<enemyAi>().agent.SetDestination(GameManager.instance.player.transform.position);
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			//animator.SetInteger("Status_walk", 0);
			inRadius = false;
			//agent.stoppingDistance = 0;
			
		}
		if (other.CompareTag("Sound"))
		{
			//animator.SetInteger("Status_walk", 0);
			//agent.stoppingDistance = 0;
			inRadius = false;
		}
	}
	
}
