using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class meleePickup : MonoBehaviour
{
	[SerializeField] meleeStats meleeStat;
    [SerializeField] playerController playerController;
    [SerializeField] TextMeshProUGUI weaponPrice;

    bool purchased;

    private void Start()
    {
        purchased = true;
        weaponPrice.text = meleeStat.weaponCost.ToString("F0");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && purchased)
        {

	        GameManager.instance.playerScript.GunPickup(meleeStat);
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

