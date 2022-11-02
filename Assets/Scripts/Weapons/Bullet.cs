using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] int damage;
    [SerializeField] int speed;
    [SerializeField] int destroyTime;
    public StatusManager statusManager;

    TargetStats targetStats;
    // Start is called before the first frame update
    void Start()
    {
        targetStats = GetComponent<TargetStats>();
        rb.velocity = transform.forward * speed;
        Destroy(gameObject, destroyTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.playerScript.takeDamage(damage);
        }
        else if (other.CompareTag("Target"))
        {
            targetStats.takeDamage(damage);
        }
        if (other.GetComponent<StatusManager>() != null)
        {
            if (statusManager != null)
            {
                other.GetComponent<StatusManager>().ApplyPosion(10);
            }
        }
        Destroy(gameObject);
    }

}
