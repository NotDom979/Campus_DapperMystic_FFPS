using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class gunPickup : MonoBehaviour
{
    [SerializeField] gunStats gunStat;
    [SerializeField] playerController playerController;
	[SerializeField] TextMeshProUGUI weaponPrice;
	public GameObject pressF;

    bool purchased;

    private void Start()
    {
		purchased = true;
        weaponPrice.text = gunStat.weaponCost.ToString("F0");
		
    }


	private void OnTriggerStay(Collider other)
	{
		playerController = other.gameObject.GetComponent<playerController>();
		if (other.CompareTag("Player") && purchased)
		{
			pressF.SetActive(true);
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
		pressF.SetActive(false);
	}

}

