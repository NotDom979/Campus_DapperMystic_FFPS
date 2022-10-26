using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gunPickup : MonoBehaviour
{
    [SerializeField] gunStats gunStat;
    [SerializeField] playerController playerController;

    bool purchased;

    private void Start()
    {
        purchased = true;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && purchased)
        {

            GameManager.instance.playerScript.GunPickup(gunStat);
            if (playerController != null)
            {
                if (playerController.purchased)
                {
                    Destroy(gameObject);
                }
                playerController.purchased = false;
            }

        }
    }
}

