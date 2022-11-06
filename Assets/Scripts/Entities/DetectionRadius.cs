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
	    enemy = gameObject.GetComponentInParent<enemyBase>().gameObject;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //animator.SetInteger("Status_walk", 1);
            enemy.GetComponent<enemyBase>().InRadius = true;
            inRadius = true;
        }
      
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            inRadius = false;
            enemy.GetComponent<enemyBase>().agent.SetDestination(enemy.GetComponent<enemyBase>().target.transform.position);

        }
       
    }
}
