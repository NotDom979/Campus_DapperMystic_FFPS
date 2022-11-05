using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    public int ammo;
    private void OnTriggerEnter(Collider other)
    {
        playerController addAmmoReserved = other.GetComponent<playerController>();


        if (addAmmoReserved)
        {
            addAmmoReserved.AddAmmoToReserved(ammo);
            Destroy(gameObject);

        }
    }
}
