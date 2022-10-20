using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float damage;
    [SerializeField] int speed;
    [SerializeField] int destroyTime;
    // Start is called before the first frame update

           enemyAi enemy;
    void Start()
    {
        rb.velocity = transform.forward * speed;
        Destroy(gameObject, destroyTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            enemy.takeDamage(damage);
            Destroy(gameObject);
        }
    }

}
