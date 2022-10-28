using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int damage;
    public StatusManager statusManager;
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
        if (other.GetComponent<StatusManager>() != null)
        {
            if (statusManager != null)
            {

                other.GetComponent<StatusManager>().ApplyAffect(6, statusManager.bleedTicks);
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

    //    if (rb != null)
    //    {
    //        Vector3 direction = collision.transform.position - transform.position;
    //        direction.y = 0;
    //        rb.AddForce(direction.normalized * knockBackStrength, ForceMode.Impulse);
    //    }
    //}
}
