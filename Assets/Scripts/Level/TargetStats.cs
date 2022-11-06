using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TargetStats : MonoBehaviour, IDamage
{
	public TextMeshProUGUI towerHealth;
    public Image enemyHpBar;
    [SerializeField]float currentHP;
	[SerializeField]float MaxHP;

    public bool destroyed;
    // Start is called before the first frame update
    void Start()
    {
	    currentHP = MaxHP;
	    towerHealth.text = currentHP.ToString("F0");
    }


    // Update is called once per frame
    void Update()
    {
        //if (round is over)
        //{
        // payDay(150)
        //}
    }
    public void payDay(int currency)
    {
       
        GameManager.instance.bankTotal += currency;
       
    }

    public void takeDamage(float damage)
	{
		currentHP -= damage;
		towerHealth.text = currentHP.ToString("F0");
        enemyHpBar.fillAmount = currentHP / MaxHP;
        if (currentHP <= 0)
        {

           Destroy(gameObject);
	        destroyed = true;
	        GameManager.instance.playerLoseMenu.SetActive(true);
	        GameManager.instance.cursorLockPause();
        }
    }

    
}
