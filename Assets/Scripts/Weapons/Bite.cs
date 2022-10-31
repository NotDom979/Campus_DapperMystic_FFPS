using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : MonoBehaviour
{
    // Start is called before the first frame update

	[SerializeField] int damage;
    //[SerializeField] float knockBackStrength;

    void Start()
    {
        
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.playerScript.takeDamage(damage);

        }
    }
}
