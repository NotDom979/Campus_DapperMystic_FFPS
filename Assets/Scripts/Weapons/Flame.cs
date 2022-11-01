using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
	[SerializeField] Rigidbody rb;
	[SerializeField] int damage;
	[SerializeField] int speed;
	[SerializeField] int destroyTime;
	
	StatusManager status;
	
	// Start is called before the first frame update
	void Start()
	{
		status = GetComponent<StatusManager>();
		rb.velocity = transform.forward * speed;
		Destroy(gameObject, destroyTime);
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") )
		{
			GameManager.instance.playerScript.takeDamage(damage);
		}

        if (other.GetComponent<StatusManager>() != null)
        {
	        other.GetComponent<StatusManager>().ApplyAffect(6, status.burnTicks);
		}
		Destroy(gameObject);
	}
   
}
